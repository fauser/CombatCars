using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace CombatCarsTestClient
{
    public partial class LogonControl : UserControl
    {
        public LogonControl()
        {
            InitializeComponent();
        }

        private void buLogon_Click(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl + "/api/authentication");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers["Authentication"] = Convert.ToBase64String((Encoding.UTF8.GetBytes(tbUserName.Text + ":" + tbPassword.Text)));

            try
            {
                using (WebResponse webResponse = request.GetResponse())
                {
                    using (Stream webStream = webResponse.GetResponseStream())
                    {
                        if (webStream != null)
                        {
                            using (StreamReader responseReader = new StreamReader(webStream))
                            {
                                string response = responseReader.ReadToEnd();
                                Token deserializedToken = JsonConvert.DeserializeObject<Token>(response);
                                Console.Out.WriteLine(response);
                                LogonCallback(deserializedToken);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine((int)((HttpWebResponse)ex.Response).StatusCode);
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
            }

            
        }

        internal void SetBaseUrl(string baseURL)
        {
            this.BaseUrl = baseURL;
        }

        private string BaseUrl { get; set; }

        public CombatCarsTestClient.LogonForm.LogonDelegate LogonCallback;
    }
}
