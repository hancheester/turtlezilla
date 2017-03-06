using Interop.BugTraqProvider;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TurtleZilla
{
    [ComVisible(true)]
#if WIN64
    [Guid("E28CE71A-CA94-4C2E-B1B4-F9BDEC472BC0")]
#else
    [Guid("6AC1A5DA-F87D-4451-A43B-BB9F5A88AD19")]
#endif
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("TurtleZilla")]
    public sealed class Plugin : IBugTraqProvider2
    {
        private IList<Issue> _issues;
        
        public string CheckCommit(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string commitMessage)
        {
            return null;
        }
        
        public string GetCommitMessage(IntPtr hParentWnd, string parameters, string commonRoot, string[] pathList, string originalMessage)
        {
            return GetCommitMessage(WindowHandleWrapper.TryCreate(hParentWnd), Parse(parameters), originalMessage);            
        }
        
        public string GetCommitMessage2(IntPtr hParentWnd, string parameters, string commonURL, string commonRoot, string[] pathList, string originalMessage, string bugID, 
                                        out string bugIDOut, out string[] revPropNames, out string[] revPropValues)
        {
            bugIDOut = bugID;
            
            // If no revision properties are to be set, 
            // the plug-in MUST return empty arrays. 

            revPropNames = new string[0];
            revPropValues = new string[0];

            return GetCommitMessage(WindowHandleWrapper.TryCreate(hParentWnd), Parse(parameters), originalMessage);            
        }

        [ComVisible(false)]
        public string GetCommitMessage(IWin32Window parentWindow, Parameters parameters, string originalMessage)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            
            try
            {
                var url = parameters.Url;
                if (url.Length == 0)
                    throw new ApplicationException("Missing Bugzilla URL.");

                var product = parameters.Product;
                if (product.Length == 0)
                    throw new ApplicationException("Missing Bugzilla product specification.");

                var apiKey = parameters.APIKey;
                if (apiKey.Length == 0)
                    throw new ApplicationException("Missing Bugzilla API key.");

                IList<Issue> issues;

                using (var dialog = new IssueBrowserDialog
                {
                    Url = url,
                    Product = product,
                    APIKey = apiKey
                })
                {
                    
                    var reply = dialog.ShowDialog(parentWindow);
                    issues = dialog.SelectedIssueObjects;

                    if (reply != DialogResult.OK || issues.Count == 0)
                        return originalMessage;

                    _issues = issues;                    
                }

                var message = new StringBuilder(originalMessage);

                if (originalMessage.Length > 0 && !originalMessage.EndsWith("\n"))
                    message.AppendLine();

                foreach (var issue in issues)
                {
                    message
                        .Append("(")
                        .Append(issue.Component).Append(" issue #")
                        .Append(issue.Id).Append(") : ")
                        .AppendLine(issue.Summary);
                }

                return message.ToString();
            }
            catch (Exception e)
            {
                ShowErrorBox(parentWindow, e);
                throw;
            }
        }

        public string GetLinkText(IntPtr hParentWnd, string parameters)
        {
            return "Bugzilla Issues";
        }

        public bool HasOptions()
        {
            return true;
        }

        public string OnCommitFinished(IntPtr hParentWnd, string commonRoot, string[] pathList, string logMessage, int revision)
        {
            return null;
        }

        public string ShowOptionsDialog(IntPtr hParentWnd, string parameters)
        {
            return ShowOptionsDialog(WindowHandleWrapper.TryCreate(hParentWnd), parameters);
        }

        [ComVisible(false)]
        public string ShowOptionsDialog(IWin32Window parentWindow, string parameterString)
        {
            Parameters parameters;

            try
            {
                parameters = Parse(parameterString);
            }
            catch (ParameterParseException e)
            {
                MessageBox.Show(e.Message, "Invalid Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return parameterString;
            }

            var dialog = new OptionDialog { Parameters = parameters };
            return dialog.ShowDialog(parentWindow) == DialogResult.OK
                ? dialog.Parameters.ToString()
                : parameterString;

        }

        public bool ValidateParameters(IntPtr hParentWnd, string parameters)
        {
            return true;
        }

        private static void ShowErrorBox(IWin32Window parent, Exception e)
        {
            MessageBox.Show(parent, 
                            e.Message,
                            e.Source + ": " + e.GetType().Name,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Parameters Parse(string str)
        {
            if (str == null) throw new ArgumentNullException("str");

            str = str.Trim();
            if (str.Length == 0)
                return new Parameters();

            var dict = ParsePairs(str.Split(';')).ToDictionary(p => p.Key, p => p.Value, StringComparer.OrdinalIgnoreCase);

            Parameters parameters;

            try
            {
                parameters = new Parameters
                {
                    Url = dict.TryPop("url"),
                    Product = dict.TryPop("product"),
                    APIKey = dict.TryPop("apikey")
                };
            }
            catch (ArgumentException e)
            {
                throw new ParameterParseException(e.Message, e);
            }

            if (dict.Any())
            {
                throw new ParameterParseException(string.Format("Parameter '{0}' is unknown.", dict.Keys.First()));
            }

            return parameters;
        }

        private IEnumerable<KeyValuePair<string, string>> ParsePairs(IEnumerable<string> parameters)
        {
            Debug.Assert(parameters != null);

            foreach (var parameter in parameters)
            {
                var pair = parameter.Split(new[] { '=' }, 2);
                var key = pair[0].Trim();
                var value = pair.Length > 1 ? pair[1].Trim() : string.Empty;
                yield return new KeyValuePair<string, string>(key, value);
            }
        }
    }
}
