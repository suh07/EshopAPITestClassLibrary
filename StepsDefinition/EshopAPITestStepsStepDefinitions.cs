using Gherkin.Ast;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace EshopAPITestClassLibrary
{
    [Binding]
    public class EshopAPITestStepsStepDefinitions
    {
        static string codeStatus;


        [When(@"User add a new item")]
        public async Task WhenUserAddANewItemAsync()
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
                description = "Tshirt-oranges",
                name = "Tshirt-redOranges",
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

            //verify the status code
            Assert.AreEqual(200, code);

            /*
            //get token
            JObject obj = JObject.Parse(output);
            string tokenValue = obj.GetValue("token").ToString();
            Console.WriteLine(tokenValue);
            */
        }



        [When(@"User will update an item details")]
        public async Task WhenUserWillUpdateAnItemDetails()
        {
            var url = "https://localhost:44339/api/catalog-items";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);

            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {   
                id = 32,
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

            Assert.AreEqual(200, code);
        }


        //----------------------------------WITH UPDATED GHERKIN---- POST - ADD A SPECIFIC ITEM -------------------------

        [Given(@"User have been autheticated with email ""([^""]*)"" and password ""([^""]*)""")]
        public void GivenUserHaveBeenAutheticatedWithEmailAndPassword(string email, string password)
        {
            var url = "https://localhost:44339/api/authenticate";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                username = email,
                password = password
            };
            var bodyy = JsonConvert.SerializeObject(body);
            request.AddBody(bodyy, "application/json");
            RestResponse response = client.Execute(request);

            var output = response.Content;
            var code = (int)response.StatusCode;

            //verify the status code
            Assert.AreEqual(200, code);
        }

        [When(@"user enter the item details with the following")]
        public void WhenUserEnterTheItemsDetailsWithTheFollowing(Table table)
        {

            var url = "https://localhost:44339/api/catalog-items";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);

            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                catalogBrandId = table.Rows[0]["catalogBrandId"],
                catalogTypeId = table.Rows[0]["catalogTypeId"],
                description = table.Rows[0]["description"],
                name = table.Rows[0]["name"],
                pictureUri = "",
                pictureBase64 = "",
                pictureName = "",
                price = Double.Parse(table.Rows[0]["price"])
            };
            var bodyy = JsonConvert.SerializeObject(body);
            request.AddBody(bodyy, "application/json");
            RestResponse response = client.Execute(request);
           // RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            Assert.AreEqual(201, code);

            codeStatus = code.ToString();
        }

        [Then(@"User response shall be equal to ""([^""]*)""")]
        public void ThenUserResponseShallBeEqualTo(string ExpectedResult)
        {
            Assert.AreEqual(ExpectedResult, codeStatus);
        }
       //-----------------------------------------------------WITH UPDATED GHERKIN POST - UPDATEA SPECIFIC ITEM------------------------------------

        [When(@"user enter the item details including id")]
        public void WhenUserEnterTheItemDetailsIncludingId(Table table)
        {
            var url = "https://localhost:44339/api/catalog-items";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Put);

            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                id = table.Rows[0]["id"],
                catalogBrandId = table.Rows[0]["catalogBrandId"],
                catalogTypeId = table.Rows[0]["catalogTypeId"],
                description = table.Rows[0]["description"],
                name = table.Rows[0]["name"],
                pictureUri = "",
                pictureBase64 = "",
                pictureName = "",
                price = Double.Parse(table.Rows[0]["price"])
            };
            var bodyy = JsonConvert.SerializeObject(body);
            request.AddBody(bodyy, "application/json");
            RestResponse response = client.Execute(request);
            // RestResponse response = await client.ExecuteAsync(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            Assert.AreEqual(200, code);

            codeStatus = code.ToString();
        }

        [Then(@"User PUT API response shall be equal to ""([^""]*)""")]
        public void ThenUserPUTAPIResponseShallBeEqualTo(string ExpectedPUTStatusCode)
        {
            Assert.AreEqual(ExpectedPUTStatusCode, codeStatus);
        }

        //----------------------------------WITH UPDATED GHERKIN POST - GET A SPECIFIC ITEM BY ID ------------------------------------


        [When(@"a user enter the id ""([^""]*)"" of an item")]
        public void WhenAUserEnterTheIdOfAnItem(string ItemID)
        {
            var url = "https://localhost:44339/api/catalog-items/";

            var URL_WithID = url + ItemID;
            var client = new RestClient(URL_WithID);
            var request = new RestRequest(URL_WithID, Method.Get);

            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);

            RestResponse response = client.Execute(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);

            codeStatus = code.ToString();

            //verfiy HTTP Status code
            Assert.AreEqual(200, code);

            //verify response headers
            string Server = response.Headers.ToList()
                                    .Find(x => x.Name == "Server")
                                    .Value.ToString();

            Assert.AreEqual("Microsoft-IIS/10.0", Server, "Server not matching");

            //verify resouce links
            var resource = request.Resource;
            Assert.AreEqual("https://localhost:44339/api/catalog-items/" + ItemID, resource);

            //verify payload
            /* JObject obj = JObject.Parse(output);
               string tokenValue = obj.GetValue("token").ToString();
               Console.WriteLine(tokenValue);
           */
        }
        //----------------------------------WITH UPDATED GHERKIN POST - DELETE A SPECIFIC ITEM BY ID ------------------------------------


        [When(@"User enter the id ""([^""]*)"" of an item")]
        public void WhenUserEnterTheIdOfAnItem(string ItemID)
        {
            var url = "https://localhost:44339/api/catalog-items/";

            var URL_WithID = url + ItemID;
            var client = new RestClient(URL_WithID);
            var request = new RestRequest(URL_WithID, Method.Delete);
            var getToken = getTokens();
            request.AddHeader("Authorization", "Bearer " + getToken);
            RestResponse response = client.Execute(request);
            var output = response.Content;
            var code = (int)response.StatusCode;
            Console.WriteLine(output);
            Console.WriteLine(code);
            codeStatus = code.ToString();

            //verfiy HTTP Status code
            Assert.AreEqual(200, code);

            //verify response headers
            string Server = response.Headers.ToList()
                                    .Find(x => x.Name == "Server")
                                    .Value.ToString();

            Assert.AreEqual("Microsoft-IIS/10.0", Server, "Server not matching");

            //verify resouce links
            var resource = request.Resource;
            Assert.AreEqual("https://localhost:44339/api/catalog-items/" + ItemID, resource);

            //verify payload
            JObject obj = JObject.Parse(output);

            // string nameValue = obj.GetValue("name").ToString();
            ///  Console.WriteLine(nameValue);
            //  Assert.AreEqual("Tshirt-redYellow", nameValue);

        }

        //-------------------------------------UPDATED GHERKIN - GET ALL ITEMS DETAILS-------------------

        [When(@"user get all items details")]
        public void WhenUserGetAllItemDetails()
        {
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
            codeStatus = code.ToString();
            Assert.AreEqual(200, code);
        }
    }
}
