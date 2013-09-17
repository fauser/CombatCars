using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CombatCarsTestClient
{
    public partial class Form1 : Form
    {
        private const string baseURL = "http://localhost:54947";

        private Token deserializedToken;
        private List<Vehicle> deserializedVehicles;


        public Form1()
        {
            InitializeComponent();
            deserializedToken = null;
            deserializedVehicles = new List<Vehicle>();
        }

        private void buGetListofCars_Click(object sender, EventArgs e)
        {
            if (deserializedToken != null && deserializedToken.ValidTo >= DateTime.Now)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/api/vehicle");
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers["AuthorizationToken"] = deserializedToken.TokenString;

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    using (Stream webStream = webResponse.GetResponseStream())
                    {
                        if (webStream != null)
                        {
                            using (StreamReader responseReader = new StreamReader(webStream))
                            {
                                string response = responseReader.ReadToEnd();

                                deserializedVehicles = (List<Vehicle>)Newtonsoft.Json.JsonConvert.DeserializeObject(response, typeof(List<Vehicle>));

                                foreach (Vehicle v in deserializedVehicles)
                                {
                                    lbListOfCars.Items.Add(v);
                                }
                                Console.Out.WriteLine(response);
                            }
                        }
                    }
                }
                catch (WebException wex)
                {
                    int statusCode = (int)((HttpWebResponse)wex.Response).StatusCode;

                    switch (statusCode)
                    {
                        case 401:
                            {
                                ShowLogonForm();
                                break;
                            }
                        default:
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("-----------------");
                    Console.Out.WriteLine(ex.Message);
                }


            }
            else
            {
                ShowLogonForm();
            }
        }

        private void ShowLogonForm()
        {
            LogonForm lf = new LogonForm(baseURL);
            lf.ShowDialog();
            deserializedToken = lf.DeserializedToken;
        }

        private void lbListOfCars_MouseClick(object sender, MouseEventArgs e)
        {
            Vehicle v = deserializedVehicles[lbListOfCars.IndexFromPoint(e.Location)];
            MessageBox.Show(v.ToString());
        }
    }
}
