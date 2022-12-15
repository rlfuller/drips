using Drips.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Drips.API.Tests
{
    internal class UserTests : BaseTest
    {
        const int userId = 2;
        const int invalidUser = 1000000000;

        /// <summary>Smoke Test to check the status of the users endpoint</summary>
        [Test]
        public void UserStatusTest()
        {
            RestRequest request = new RestRequest($"/users/{userId}", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        /// <summary>Validate that the requested user is returned</summary>
        [Test]
        public void UserDataTestWithValidUser()
        {
            RestRequest request = new RestRequest($"users/{userId}", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            UserBody body = JsonConvert.DeserializeObject<UserBody>(response.Content);

            Assert.That(body.user.Id, Is.EqualTo(userId));
        }

        /// <summary>Validate that request for invalid user fails</summary>
        [Test]
        public void UserDataTestWithInvalidUser()
        {
            RestRequest request = new RestRequest($"users/{invalidUser}", Method.Get);

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            UserBody body = JsonConvert.DeserializeObject<UserBody>(response.Content);

            Assert.That(body.user, Is.Null);
        }


        /// <summary>
        /// Checks the pagination results from the api
        /// </summary>
        /// <param name="lastPage">The page received should be the last page of the set</param>
        /// <param name="usersPerPage">Number of users per page</param>
        [TestCase(12, 1, TestName = "One user per page, last page has status OK.")]
        [TestCase(3, 5, TestName = "Five users per page, last page has status OK.")]
        public void UserListDataPaginationWithValidArgumentsTest(int lastPage, int usersPerPage)
        {
            //int lastPage = totalPages;
            RestRequest request = new RestRequest($"users?page={lastPage}&per_page={usersPerPage}", Method.Get);
            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            UserListBody body = JsonConvert.DeserializeObject<UserListBody>(response.Content);


            // Check that the total number of users that could be presented per page is correct
            Assert.That(body.PerPage, Is.EqualTo(usersPerPage));

            //Check that the number of users per page is correct based on the last page
            //Check that the total number of pages is correct
            if (body.TotalUsers % usersPerPage == 0)
            {
                Assert.That(body.Users.Count, Is.EqualTo(usersPerPage));
                Assert.That(body.TotalPages, Is.EqualTo(body.TotalUsers / usersPerPage));
            }
            else
            {
                Assert.That(body.Users.Count, Is.EqualTo(body.TotalUsers % usersPerPage));
                Assert.That(body.TotalPages, Is.EqualTo((body.TotalUsers / usersPerPage) + 1));
            }
            
        }

        /// <summary>
        /// Ensures that api should return appropriate status code when arguments are invalid
        /// If we send a negative page count, we would expect a 400 response
        /// </summary>
        /// <param name="usersPerPage">Number of users per page</param>
        /// <param name="page">The requested page (current page)</param>
        [TestCase(3, 0, TestName = "0 current page, status should be 400.")]
        [TestCase(-4, 1, TestName = "Negative users per page, status should be 400.")]
        [TestCase(3, -1, TestName = "Negative page number, status should be 400.")]
        public void UserListDataPaginationWithInvalidArgumentsFails(int usersPerPage, int page)
        {
            RestRequest request = new RestRequest($"users?page={page}&per_page={usersPerPage}", Method.Get);
            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        /// <summary>
        /// Ensure that api returns in a reasonable amount of time or else throws a timeout exception
        /// </summary>
        /// <param name="shouldFail">Boolean that indicates if we are expecting the api to timeout or not based on the configuration of a max timeout in the request</param>
        /// <param name="timeout">Indicates how long to wait before a timeout</param>
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


        /// <summary>
        /// Validate that only requested type of data is received from api request by setting the Accept header to only accept xml. In this case, we would not expect JSON to be returned. If xml is not supported, we would expect an error to be returned.
        /// </summary>
        [Test]
        public void UserInvalidAcceptHeaderTest()
        {
            RestRequest request = new RestRequest($"users/{userId}", Method.Get);
            request.AddHeader("Accept", "application/xml");

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotAcceptable));
        }

        /// <summary>
        /// Validate that Create User is successful 
        /// </summary>
        [Test]
        public void CreateUserIsSuccessful()
        {
            RestRequest request = new RestRequest("users", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new { name = "Chester", job = "my dog" }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created)) ;

            UserCreateResponseBody body = JsonConvert.DeserializeObject<UserCreateResponseBody>(response.Content);

            Assert.That(body.Name, Is.EqualTo("Chester"));
            int chesterUserId = body.Id;

            //make sure we can get the newly creatd user (that it really exists)
            request = new RestRequest($"/users/{chesterUserId}", Method.Get);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Valid that create user is unsuccessful if payload is missing
        /// </summary>
        [Test]
        public void CreateUserWithEmptyPayloadIsNotSuccessful()
        {
            RestRequest request = new RestRequest("users", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new { }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        /// <summary>
        /// Validate that create user is unsuccessful if content-type header is missing. 
        /// </summary>
        [Test]
        public void CreateUserWithMissingContentTypeIsNotSuccessful()
        {
            RestRequest request = new RestRequest("users", Method.Post);

            request.AddBody(request.AddJsonBody(new { name = "June", job = "my other dog" }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }

        /// <summary>
        /// Validate that update user is successful given a valid user
        /// </summary>
        [Test]
        public void UpdateUserIsSuccessful()
        {
            RestRequest request = new RestRequest($"users/{userId}", Method.Put);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new { name = "Chester", job = "my dog" }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            UserUpdateResponseBody body = JsonConvert.DeserializeObject<UserUpdateResponseBody>(response.Content);

            Assert.That(body.Name, Is.EqualTo("Chester"));
            
            //Ideally, we would then GET the updated user from the list of users and check the
            //value of the field that was updated, but the Name field isn't listed on the users endpoint, 
            //it's only there for the crud endpoints
        }

        /// <summary>
        /// Given a valid user, validate that delete user is successful. For this test, we will first crete a new user, then delete them
        /// </summary>
        [Test]
        public void DeleteUserIsSuccessful()
        {
            RestRequest request = new RestRequest($"users/{userId}", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new { name = "TestDeleteUser", job = "Test" }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            UserCreateResponseBody body = JsonConvert.DeserializeObject<UserCreateResponseBody>(response.Content);

            int testDeleteUserId = body.Id;

            request = new RestRequest($"users/{testDeleteUserId}", Method.Delete);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            //Now try to get the id and verify that a 404 is returned
            request = new RestRequest($"users/{testDeleteUserId}", Method.Get);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }

    }
}
