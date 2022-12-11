using Drips.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

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

        [Test]
        public void HelloWorldUserListDataTest()
        {
            // arrange

            RestRequest request = new RestRequest($"users?page=5&per_page=1", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            UserListBody body = JsonConvert.DeserializeObject<UserListBody>(response.Content);

            // assert
            //Assert.That(body.user.Id, Is.EqualTo(2));

        }

        [TestCase(999, true, TestName = "Delay times out")]
        [TestCase(2000, false, TestName = "Responds in a reasonable timeframe")]
        public void DelayedResponseUserTest(int timeout, bool shouldFail)
        {

            var options = new RestClientOptions(config.ApiBaseUrl)
            {
                ThrowOnAnyError = true,
                MaxTimeout = timeout //ms
            };
            var client = new RestClient(options);
            

            RestRequest request = new RestRequest("users?delay=1", Method.Get);

            if (shouldFail)
            {
                Assert.Throws<TimeoutException>(() => client.Execute(request));
            } 
            else
            {
                RestResponse response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
    }
}
