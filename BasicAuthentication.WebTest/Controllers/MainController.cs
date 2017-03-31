using BasicAuthentication.Users;
using BasicAuthentication.WebTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BasicAuthentication.WebTest.Controllers
{
    [RoutePrefix("api/v1")]
    public class MainController : ApiController
    {
        static MainController()
        {
            var steve = new TestUser()
            {
                UserName = "Steve",
                Email = "steve@gmail.com",
                EmailConfirmed = true
            };
            
        }

        [HttpGet]
        [Route("initializeSystem")]
        [Authorize]
        public IHttpActionResult InitializeSystem()
        {
            var json = new
            {
                LoggedIn = true
            };
            return Json(json);
        }

        [HttpGet]
        [Route("pingTest")]
        [Authorize]
        public IHttpActionResult PingTest()
        {
            var json = new
            {
                Success = true
            };
            return Json(json);
        }
    }
}