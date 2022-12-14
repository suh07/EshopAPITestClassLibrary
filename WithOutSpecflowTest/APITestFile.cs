using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAPITestClassLibrary
{
    [TestClass]
    public class APITestFile
    {
        [TestMethod]
        /*
        public async Task WhenActionIsPOSTAsync()
        {
            var url = "https://localhost:44339/api/catalog-items";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);

            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                catalogBrandId = 2,
                catalogTypeId = 2,
                description = "Tshirt-orange",
                name = "Tshirt-redOrange",
                pictureUri = "",
                pictureBase64 = "",
                pictureName = "",
                price = 215.0
            };
            var bodyy = JsonConvert.SerializeObject(body);
            request.AddBody(bodyy, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            Assert.AreEqual(201, code);
        }
        */
        public void WhenUserGetAllItemDetails()
        {
            //var url = "https://localhost:44339/api/catalog-brands";

            var url = "https://localhost:44339/api/catalog-items";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);
            RestResponse response = client.Execute(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);
            //codeStatus = code.ToString();
            Assert.AreEqual(200, code);

            var Output = response.Content;
        }

        private string getTokens()
        {
            var url = "https://localhost:44339/api/authenticate";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                username = "admin@microsoft.com",
                password = "Pass@word1"
            };
            var bodyy = JsonConvert.SerializeObject(body);
            request.AddBody(bodyy, "application/json");
            RestResponse response = client.Execute(request);
            //RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            Assert.AreEqual(200, code);

            //get token
            JObject obj = JObject.Parse(output);
            string tokenValue = obj.GetValue("token").ToString();
            Console.WriteLine(tokenValue);

            return tokenValue;
        }
    }
}
