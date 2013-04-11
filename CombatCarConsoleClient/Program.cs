using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CombatCarConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            //Http();

            //Local();

        }

        private static void Local()
        {
            CombatCarsConsloeClientModelDataContext repository = new CombatCarsConsloeClientModelDataContext(@"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234");

            User user = repository.Users.Single(u => u.UserId == 1);

            Vehicle v = new Vehicle() { Name = "Beaver" };

            UserVehicle uv = new UserVehicle() { User = user, Vehicle = v };


            repository.SubmitChanges();

        }

        private static void Http()
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54947/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = client.GetAsync("api/vehicle").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                /*
                // Parse the response body. Blocking!
                var products = response.Content.re ReadAsAsync<IEnumerable<Vehicle>>().Result;
                foreach (var p in products)
                {
                    Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.Price, p.Category);
                }*/
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }


            // Create a new product
            var gizmo = new Vehicle() { Name = "Yalla" };
            Uri gizmoUri = null;


            // Create the JSON formatter.
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();

            // Use the JSON formatter to create the content of the request body.
            HttpContent content = new ObjectContent<Vehicle>(gizmo, jsonFormatter);

            // Send the request.
            response = client.PostAsync("api/vehicle", content).Result;

            if (response.IsSuccessStatusCode)
            {
                gizmoUri = response.Headers.Location;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
