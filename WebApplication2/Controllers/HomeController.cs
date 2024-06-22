using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace WeatherForecastController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginrequest)
        {
            var result = new
            {
             
                Status = "success",
                version = "1.0",
                username = loginrequest.Username,
                password = loginrequest.Password,
            };

            return Ok(result);
        }
        [HttpPost("user")]
        public IActionResult User(LoginRequest loginrequest)
        {
            var result = new
            {

                Status = "success",
                version = "1.0",
                username = loginrequest.Username,
                password = loginrequest.Password,
            };

            return Ok(result);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}

