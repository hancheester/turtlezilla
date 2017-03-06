using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace TurtleZilla.HttpUtility
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    
    public class RestJsonClient
    {
        private static ManualResetEvent _allDone = new ManualResetEvent(false);

        private IAsyncResult _currentAsyncResult;

        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public string ResponseValue { get; private set; }

        public event EventHandler<int> ProgressChanged;
        public event EventHandler<RestJsonClient> DownloadCompleted;

        public RestJsonClient()
        {
            EndPoint = string.Empty;
            Method = HttpVerb.GET;
            ContentType = "application/json";
            PostData = string.Empty;

            SetAllowUnsafeHeaderParsing20();
        }

        public RestJsonClient(string endPoint, HttpVerb method) 
            : this()
        {
            EndPoint = endPoint;
            Method = method;
            ContentType = "application/json";
            PostData = string.Empty;
        }

        public RestJsonClient(string endPoint, HttpVerb method, string postData) 
            : this()
        {
            EndPoint = endPoint;
            Method = method;
            ContentType = "application/json";
            PostData = postData;
        }

        public RestJsonClient SendRequest(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
                parameters = "?" + parameters;

            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            
            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            request.Accept = ContentType;
            request.KeepAlive = false;
            request.Proxy = null;
            
            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = string.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                    }
                }

                ResponseValue = responseValue;
            }

            return this;
        }        
        
        public T Serialize<T>()
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(ResponseValue)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }

        public void SendRequestAsync(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
                parameters = "?" + parameters;

            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            request.Accept = ContentType;
            request.KeepAlive = false;
            request.Proxy = null;

            switch (Method)
            {
                case HttpVerb.POST:
                case HttpVerb.PUT:
                    _currentAsyncResult = request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
                    break;

                case HttpVerb.GET:
                case HttpVerb.DELETE:
                default:
                    _currentAsyncResult = request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
                    break;
            }
            
            // Wait until the ManualResetEvent is set so that the application   
            // does not exit until after the callback is called.  
            _allDone.WaitOne();
        }
        
        public void Abort()
        {
            if (_currentAsyncResult != null)
            {
                ((HttpWebRequest)_currentAsyncResult).Abort();
            }
        }

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            var request = (HttpWebRequest)asynchronousResult.AsyncState;
            
            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var stream = request.EndGetRequestStream(asynchronousResult))
                {
                    stream.Write(bytes, 0, bytes.Length);                    
                }
            }

            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);

        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            var request = (HttpWebRequest)asynchronousResult.AsyncState;

            using (var response = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = string.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                long contentLength = response.ContentLength;

                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var bytes = GetContentWithProgressReporting(stream, contentLength);
                        ResponseValue = Encoding.GetEncoding("iso-8859-1").GetString(bytes);
                    }
                }

                //using (var stream = response.GetResponseStream())
                //{
                //    if (stream != null)
                //    {
                //        using (var reader = new StreamReader(stream))
                //        {
                //            ResponseValue = reader.ReadToEnd();
                //        }
                //    }
                //}
            }

            _allDone.Set();

            DownloadCompletedHandler();            
        }

        private byte[] GetContentWithProgressReporting(Stream responseStream, long contentLength)
        {
            UpdateProgressBar(0);

            var data = new byte[contentLength];
            int currentIndex = 0;
            int bytesReceived = 0;
            var buffer = new byte[256];

            do
            {
                bytesReceived = responseStream.Read(buffer, 0, 256);
                Array.Copy(buffer, 0, data, currentIndex, bytesReceived);
                currentIndex += bytesReceived;

                double percentage = (double)currentIndex / contentLength;
                UpdateProgressBar((int)(percentage * 100));

            } while (currentIndex < contentLength);

            UpdateProgressBar(100);
            return data;
        }

        private void UpdateProgressBar(int percentage)
        {
            ProgressChanged?.Invoke(this, percentage);
        }

        private void DownloadCompletedHandler()
        {
            DownloadCompleted?.Invoke(this, this);
        }
        
        private static bool SetAllowUnsafeHeaderParsing20()
        {
            //Get the assembly that contains the internal class
            Assembly aNetAssembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            if (aNetAssembly != null)
            {
                //Use the assembly in order to get the internal type for the internal class
                Type aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
                if (aSettingsType != null)
                {
                    //Use the internal static property to get an instance of the internal settings class.
                    //If the static instance isn't created allready the property will create it for us.
                    object anInstance = aSettingsType.InvokeMember("Section",
                      BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });

                    if (anInstance != null)
                    {
                        //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
                        FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (aUseUnsafeHeaderParsing != null)
                        {
                            aUseUnsafeHeaderParsing.SetValue(anInstance, true);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
