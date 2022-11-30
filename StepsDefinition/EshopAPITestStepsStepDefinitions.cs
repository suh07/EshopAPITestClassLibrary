using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace EshopAPITestClassLibrary
{
    [Binding]
    public class EshopAPITestStepsStepDefinitions
    {
       

        [When(@"action is GET")]
        public void WhenActionIsGET()
        {
            var url = "https://localhost:44339/api/catalog-brands";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            RestResponse response = client.Execute(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            Assert.AreEqual(200, code);
        }

        [When(@"action is POST")]
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

        [When(@"a user enter the input the ""([^""]*)"" for an item")]
        public void WhenAUserEnterTheInputTheForAnItem(string id)
        {
            var url = "https://localhost:44339/api/catalog-items/";

            var URL_WithID = url + id;
            var client = new RestClient(URL_WithID);
            var request = new RestRequest(URL_WithID, Method.Get);
            RestResponse response = client.Execute(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            //verfiy HTTP Status code
            Assert.AreEqual(200, code);

            //verify response headers
            string Server = response.Headers.ToList()
                                    .Find(x => x.Name == "Server")
                                    .Value.ToString();

            Assert.AreEqual("Microsoft-IIS/10.0", Server, "Server not matching");

            //verify resouce links
            var resource = request.Resource;
            Assert.AreEqual("https://localhost:44339/api/catalog-items/1", resource);

            //verify payload
            JObject obj = JObject.Parse(output);
            string tokenValue = obj.GetValue("token").ToString();
            Console.WriteLine(tokenValue);
        }

        [When(@"action is POST authenticate user")]
        public void WhenActionIsPOSTAuthenticateUser()
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

            /*
            //get token
            JObject obj = JObject.Parse(output);
            string tokenValue = obj.GetValue("token").ToString();
            Console.WriteLine(tokenValue);
            */
        }


    }
}
