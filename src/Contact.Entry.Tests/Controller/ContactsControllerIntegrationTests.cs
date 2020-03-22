using ContactEntry.Api;
using ContactEntry.Api.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace ContactEntry.Tests.Controller
{
    public class ContactsControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ContactsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task Can_POST_Contact()
        {
            //Arrange
            string json = @"{
                                ""name"": {
                                    ""first"": ""Harold"",
                                    ""middle"": ""Francis"",
                                    ""last"": ""Gilkey""
                                  },
                                  ""address"": {
                                    ""street"": ""8360 High Autumn Row"",
                                    ""city"": ""Cannon"",
                                    ""state"": ""Delaware"",
                                    ""zip"": ""19797""
                                  },
                                  ""phone"": [
                                    {
                                      ""number"": ""302-611-9148"",
                                      ""type"": ""home""
                                    },
                                    {
                                      ""number"": ""302-532-9427"",
                                      ""type"": ""mobile""
                                    }
                                  ],
                                  ""email"": ""harold.gilkey@yahoo.com""
                                }
                                ";

            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //Act

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/contacts", httpRequestMessage.Content);
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Contact>(stringResponse);

            //Assert
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task Do_Not_Allow_Unsupported_Phone_Type_Contact()
        {
            //Arrange
            string json = @"{
                                    ""phone"": [
                                    {
                                      ""number"": ""302-611-9148"",
                                      ""type"": ""unkown""
                                    }
                                  ]
                                }
                                ";

            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //Act

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/contacts", httpRequestMessage.Content);
            // Must be successful.
            // httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.Contains("Please enter one of the allowable values: home, work, mobile.", stringResponse); // (stringResponse.Contains("")); // ();
        }

        [Fact]
        public async Task Can_GET_Contact_By_Id()
        {
            //Arrange
            int contactId = 1;

            //Act
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/contacts/" + contactId);
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Contact>(stringResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(result.Name != null);
        }

        [Fact]
        public async Task Can_PUT_Contact()
        {
            //Arrange
            string json = @"{  
                                ""id"":2,
                                ""name"": {
                                    ""first"": ""Harold"",
                                    ""middle"": ""Francis555555"",
                                    ""last"": ""Gilkey""
                                  },
                                  ""address"": {
                                    ""street"": ""8360 High Autumn Row"",
                                    ""city"": ""Cannon"",
                                    ""state"": ""Delaware"",
                                    ""zip"": ""19797""
                                  },
                                  ""phone"": [
                                    {
                                      ""number"": ""302-611-9148"",
                                      ""type"": ""home""
                                    },
                                    {
                                      ""number"": ""302-532-9427"",
                                      ""type"": ""mobile""
                                    }
                                  ],
                                  ""email"": ""harold.gilkey@yahoo.com""
                                }
                                ";

            var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //Act
            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync("/contacts/2", httpRequestMessage.Content);
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Can_DELETE_Contact_By_Id()
        {
            //Arrange
            int contactId = 3;

            //Act
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync("/contacts/" + contactId);
            // Must be successful.
            // httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Contact>(stringResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(result.Id == contactId);
        }

        [Fact]
        public async Task Can_GET_ALL_Contacts()
        {
            //Arrange
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/contacts");
            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            //Act
            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var contacts = JsonConvert.DeserializeObject<IEnumerable<Contact>>(stringResponse);

            //Assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.Contains(contacts, p => p.Name.First == "Harold");
            Assert.Contains(contacts, p => p.Name.Last == "Gilkey");
        }
    }
}
