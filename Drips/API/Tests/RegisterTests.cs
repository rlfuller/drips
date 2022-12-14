using Drips.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Drips.API.Tests
{
    internal class RegisterTests : BaseTest
    {
        /// <summary>
        /// Validate that registration is successful. After registering, we should be able to call the login with the new credentials and login successfully.
        /// </summary>
        [Test]
        public void RegistrationIsSuccessful()
        {
            RestRequest request = new RestRequest("register", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new
            {
                email = "eve.holt@reqres.in",
                password = "testpassword1"
            }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            RegisterResponseBody body = JsonConvert.DeserializeObject<RegisterResponseBody>(response.Content);

            Assert.That(body, Is.Not.Null);
            Assert.That(body.Token, Is.Not.Empty);

            //now try to login since we are registered
            request = new RestRequest("login", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new
            {
                email = "eve.holt@reqres.in",
                password = "testpassword1"
            }));

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            LoginResponseBodySuccessful loginBody = JsonConvert.DeserializeObject<LoginResponseBodySuccessful>(response.Content);

            Assert.That(loginBody, Is.Not.Null);
            Assert.That(loginBody.Token, Is.Not.Empty);

        }

        /// <summary>
        /// Valdiate that registration with invalid credentials is not successful
        /// </summary>
        /// <param name="email">Can be invalid for email credentials are incorrect</param>
        /// <param name="password">Can be invalid for password tests</param>
        [TestCase("test@gmail-.com", "registerpassword1", TestName = "Login Fails with invalid Email")]
        [TestCase(null, "registerpassword1", TestName = "Login Fails with missing Email")]
        [TestCase("sydney@fife", null, TestName = "Login Fails with Missing Password")]
        public void RegistrationIsNotSuccessfulWithInvalidorMissingCredentials(string email, string password)
        {
            RestRequest request = new RestRequest("login", Method.Post);

            request.AddHeader("Content-type", "application/json");

            request.AddBody(request.AddJsonBody(new
            {
                email = email,
                password = password
            }));

            RestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

        }
    }
}
