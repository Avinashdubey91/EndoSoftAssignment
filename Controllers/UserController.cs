using EndoSoftAssignment.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndoSoftAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public readonly UserContext _userContext;
        public UserController(IConfiguration configuration, UserContext userContext)
        {
            _configuration = configuration;
            _userContext = userContext;

        }

        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if(_userContext.Users.Where(x => x.Mobile == user.Mobile).FirstOrDefault() != null)
            {
                return Ok("Already Exist");
            }

            user.CreateDate = DateTime.Now;
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
            return Ok("Success");
        }

        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            var checkValidUser = _userContext.Users.Where(x => x.Mobile == user.MobileNo && x.Password == user.Password).FirstOrDefault();
            if (checkValidUser != null)
            {
                return Ok("Success");
            }
            return Ok("Failure");
        }

        [HttpGet("UserList")]
        public JsonResult GetUsers()
        {
            var userInfo = _userContext.Users.ToList();

            return new JsonResult(userInfo);
        }
    }
}
