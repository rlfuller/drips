using Drips.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Drips.API.Tests
{
    internal class LoginTests : BaseTest
    {

        /// <summary>
        /// Validate that login with valid credentials is successful
        /// </summary>
        /// <param name="password">Can be ascii or unicode</param>
        [TestCase("cityslicka", TestName = "Login succeeds with ASCII password")]
        [TestCase("påssw0rd!", TestName = "Login succeeds with Unicode password")]
        public void LoginIsSuccessful(string password)
        {
            RestRequest request = new RestRequest("login", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new { email = "eve.holt@reqres.in",
                                                      password = password
            }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            LoginResponseBodySuccessful body = JsonConvert.DeserializeObject<LoginResponseBodySuccessful>(response.Content);

            Assert.That(body, Is.Not.Null);
            Assert.That(body.Token, Is.Not.Empty);

        }

        /// <summary>
        /// Validate that if password is invalid or missing, we do not login
        /// </summary>
        /// <param name="password">Password should not be valid for this test</param>
        [TestCase("incorrect password", TestName = "Login Fails with Incorrect Password")]
        [TestCase("", TestName = "Login Fails with empty Password")]
        [TestCase(null, TestName = "Login Fails with Missing Password")]
        public void LoginIsNotSuccessfulWithInvalidorMissingPassword(string password)
        {
            RestRequest request = new RestRequest("login", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new
            {
                email = "eve.holt@reqres.in",
                password = password
            }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }
    }
}
