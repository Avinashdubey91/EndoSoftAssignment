using EndoSoftAssignment.Models;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login(Login user)
        {
            var checkValidUser = _userContext.Users.Where(x => x.Mobile == user.MobileNo && x.Password == user.Password).FirstOrDefault();
            if (checkValidUser != null)
            {
                return Ok(new JWTService(_configuration).GenerateJWTToken(
                        checkValidUser.UserId.ToString(),
                        checkValidUser.FirstName, 
                        checkValidUser.LastName,
                        checkValidUser.Address,
                        checkValidUser.Mobile
                    ));
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
