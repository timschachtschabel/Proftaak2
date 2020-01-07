﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace BeepWPFApp.Classes
{
    /// <summary>
    /// API Controller class voor interactie met .NET Core WEB API
    /// </summary>
    class api
    {
        //URL voor API endpoints
        private string ProductEndpoint = "http://localhost:5050/api/products?";
        private string UserEndpoint = "http://localhost:5050/api/user?";
        //auth token
        private string jwt = "";

        //Krijg product van API
        public Product GetProduct(string barcode)
        {

            string url = ProductEndpoint+ $"barcode={barcode}";
            //Als we unauthed error krijgen, verkrijg nieuwe JWT
            while (Authed(url) == false)
            {
                Auth();
            }
            var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", "Bearer " + jwt);
            var response = client.Execute(new RestRequest());

            //Zet JSON om in Product
            Product resultProduct = JsonConvert.DeserializeObject<Product>(response.Content);

            //Zet de string om in een list
            char[] seperator = ".".ToCharArray();
            if (resultProduct != null)
            {
                if (resultProduct.allergie != null)
                {
                    resultProduct.AllergieList = resultProduct.allergie.Split(seperator).ToList();
                }

                if (resultProduct.ingredient != null)
                {
                    resultProduct.IngredientList = resultProduct.ingredient.Split(seperator).ToList();
                }

                resultProduct.naam = resultProduct.naam.Replace("&#39;", "'");
            }
            
            //return het eindproduct
            return resultProduct;
        }

        public User GetUser(string naam, string password)
        {
            //URL voor API Endpoint
            string url = UserEndpoint + $"naam={naam}&password={password}";
            //Als we een 401 krijgen, dan nieuwe JWT Token.
            while (Authed(url) == false)
            {
                Auth();
            }
            var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", "Bearer " + jwt);
            var response = client.Execute(new RestRequest());


            //JSON omzetten naar USER
            User resultProduct = JsonConvert.DeserializeObject<User>(response.Content);
            return resultProduct;
        }


        public bool CreateUser(string naam, string password, string email, List<string> Allergie)
        {
            //API Enpoint
            string url = UserEndpoint + $"Naam={naam}&Email={email}&password={password}&allergie={Allergie}";

                //maakt van List string
                string allergie = "";
                foreach (var item in Allergie)
                {
                    allergie = allergie + item + ",";
                }

                //Als 401 dan JWT token verkrijgen
                while (Authed(url) == false)
                {
                    Auth();
                }

                var client = new RestClient(url);
                client.AddDefaultHeader("Authorization", "Bearer " + jwt);
                var response = client.Execute(new RestRequest(Method.POST));

                //Return true als het goed gaat
                if (response.StatusCode == HttpStatusCode.OK) return true;
                return false;
        }


        //Check als we geen 401 krijgen
        private bool Authed(string url)
        {
            var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", "Bearer " + jwt);
            var response = client.Execute(new RestRequest());

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return false;

            return true;
        }

        //verkrijg nieuwe token
        private void Auth()
        {
            var authClient = new RestClient("http://localhost:5050/api/auth/token");
                
            var authResponse = authClient.Execute(new RestRequest(Method.POST));

            var result = JsonConvert.DeserializeObject(authResponse.Content);

            jwt = result.ToString();
        }
    }
}