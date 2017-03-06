using System;
using System.Net;
using System.Windows.Forms;
using TurtleZilla.Data;
using TurtleZilla.HttpUtility;

namespace TurtleZilla
{
    public partial class OptionDialog : Form
    {
        private Parameters _parameters;

        public Parameters Parameters
        {
            get
            {
                if (_parameters == null) _parameters = new Parameters();
                return _parameters;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _parameters = value;
            }
        }

        public OptionDialog()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Text = "Options - " + Parameters.Product;
            textApiKey.Text = Parameters.APIKey;
            textWebAddress.Text = Parameters.Url;
            base.OnLoad(e);
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                var endPoint = textWebAddress.Text.Trim().TrimEnd('/');
                var apiKey = textApiKey.Text.Trim();
                var client = new RestJsonClient(endPoint + BugzillaAPI.VERSION, HttpVerb.GET);
                var data = client.SendRequest(string.Format("api_key={0}", apiKey)).Serialize<Bugzilla>();
                var version = data.version;

                client = new RestJsonClient(endPoint + BugzillaAPI.PRODUCT + "/" + Parameters.Product, HttpVerb.GET);
                data = client.SendRequest(string.Format("api_key={0}", apiKey)).Serialize<Bugzilla>();

                var productStatus = "";
                if (data.products.Count == 0)
                {
                    productStatus = string.Format("Product '{0}' is not found. Please make sure the product has been created.", Parameters.Product);
                }
                else
                {
                    productStatus = string.Format("Product '{0}' is {1}.", Parameters.Product, data.products[0].is_active ? "active" : "inactive");
                }

                var message = string.Format("The Bugzilla version is {0}.{1}{2}{3}", 
                    version, 
                    Environment.NewLine,
                    Environment.NewLine,
                    productStatus);

                MessageBox.Show(message, "Test Passed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (WebException we)
            {
                MessageBox.Show(we.Message, "Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
