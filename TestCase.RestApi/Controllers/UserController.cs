using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestCase.Business;
using TestCase.Business.Implementation;
using TestCase.Model;

namespace TestCase.RestApi.Controllers
{
    [RoutePrefix("testcase/user")]
    public class UserController: ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(UserManager userManager) // :base class for authorization can be used here.
        {
            _userManager = userManager;
        }

        // GET User By Id
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetUserById(int userId)
        {
            var result = _userManager.GetUserById(userId);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST User
        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody] User user)
        {
            var result = _userManager.CreateUser(user);
            if (!result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        // PUT
        [Route("")]
        [HttpPut]
        public HttpResponseMessage UpdateUser(int userId, [FromBody] User user)
        {
            var result = _userManager.UpdateUser(userId, user);
            if (!result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        // DELETE
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int userId)
        {
            var result = _userManager.DeleteUser(userId);
            if (!result)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }
    }
}
