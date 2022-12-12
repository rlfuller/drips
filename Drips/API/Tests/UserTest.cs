using Drips.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Drips.API.Tests
{
    internal class UserTest : BaseTest
    {
        const int userId = 2;

        [Test]
        public void UserStatusTest()
        {
            RestRequest request = new RestRequest($"/users/{userId}", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void UserDataTest()
        {
            RestRequest request = new RestRequest($"users/{userId}", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            UserBody body = JsonConvert.DeserializeObject<UserBody>(response.Content);

            Assert.That(body.user.Id, Is.EqualTo(2));
        }

        [TestCase(12, 1, TestName = "One user per page, last page has status OK.")]
        [TestCase(3, 5, TestName = "Five users per page, last page has status OK.")]
        public void UserListDataTest(int expectedPages, int perPage)
        {
            RestRequest request = new RestRequest($"users?page={expectedPages}&per_page={perPage}", Method.Get);
            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            UserListBody body = JsonConvert.DeserializeObject<UserListBody>(response.Content);

            // need assertion on the pagination
            Assert.That(body.PerPage, Is.EqualTo(perPage));
            Assert.That(body.Users.Count, Is.EqualTo(body.TotalUsers / perPage));
            Assert.That(body.TotalUsers / perPage, Is.EqualTo(body.TotalPages));

            request = new RestRequest($"/users?page={expectedPages + 1}&perPage={perPage}", Method.Get);
            response = client.Execute(request);
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

        [Test]
        public void UserInvalidAcceptHeaderTest()
        {
            RestRequest request = new RestRequest("users/2", Method.Get);
            request.AddHeader("Accept", "application/xml");

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotAcceptable));
        }
    }
}
