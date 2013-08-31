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

        private void buLogon_Click(object sender, EventArgs e)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseURL + "/api/authentication");
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers["Authentication"] = Convert.ToBase64String((Encoding.UTF8.GetBytes("daniel:ds1234")));

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
                            deserializedToken = JsonConvert.DeserializeObject<Token>(response);
                            Console.Out.WriteLine(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
            }
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
                catch (Exception ex)
                {
                    Console.Out.WriteLine("-----------------");
                    Console.Out.WriteLine(ex.Message);
                }


            }
        }

        private void lbListOfCars_MouseClick(object sender, MouseEventArgs e)
        {
            Vehicle v = deserializedVehicles[lbListOfCars.IndexFromPoint(e.Location)];

            MessageBox.Show(v.ToString());
        }
    }
}
