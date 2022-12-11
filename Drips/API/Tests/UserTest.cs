using Drips.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Drips.API.Tests
{
    internal class UserTest : BaseTest
    {
        [Test]
        public void HelloWorldUser()
        {
            // arrange
            
            RestRequest request = new RestRequest($"users/2", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void HelloWorldUserDataTest()
        {
            // arrange
            
            RestRequest request = new RestRequest($"users/2", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            UserBody body = JsonConvert.DeserializeObject<UserBody>(response.Content);

            // assert
            Assert.That(body.user.Id, Is.EqualTo(2));

        }
    }
}
