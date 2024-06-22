using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.Eventing.Reader;
using WebApplication2.Models.Request;
using WebApplication2.Models.Response;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        SignupRequest request = new SignupRequest();
        private readonly IMongoCollection<BsonDocument> _usercollection;
        public MemberController(IMongoClient client)
        {
            var database = client.GetDatabase("DevEX-DB");
            _usercollection = database.GetCollection<BsonDocument>("Users");

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(SignupRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Firstname) && !string.IsNullOrEmpty(request.Password) && !string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Mobile))
                {
                   

                    var document = new BsonDocument

                {

                    { "Firstname", request.Firstname },
                    { "Password", request.Password },
                    { "Email", request.Email },
                    { "Lastname",request.Lastname},
                    { "Mobile", request.Mobile }


                };
                    await _usercollection.InsertOneAsync(document);



                    SignupResponse response = new SignupResponse()
                    {
                        Message = "User has been created",
                        Status = "OK",
                       

                    };

                    return Ok(response);
                }
                else
                {
                    SignupResponse response = new SignupResponse()
                    {
                        Message = "Invalid request",
                        Status = "Not OK"
                    };
                    return BadRequest(response);
                }

            }
            catch (Exception ex) {
                
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while processing your request.");


            }

        }   
        [HttpPost("ValidateUser")]
       public async Task<IActionResult> ValidateUser(string Email, string Password)
        {
            var filter = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Eq("Email", Email ),Builders<BsonDocument>.Filter.Eq("Password", Password))      ;
            var userdata = await _usercollection.Find(filter).FirstOrDefaultAsync();

            if (userdata != null && userdata.Count() > 0 )
            {
                SignupResponse response = new SignupResponse()
                {
                    Message = "logged in successfully",
                    Status = "OK"
                };
                return Ok(response);
            }
            else
            {
               SignupResponse response = new SignupResponse()
                {
                    Message = "Invalid user",
                    Status = "Not OK"
                };
                return Unauthorized(response);
            }
           
        }

        [HttpGet]
        [Route("/UserDetails/{username}")]
        public async Task<IActionResult> UserDetails(string username)
        {
            
           
                var filter = Builders<BsonDocument>.Filter.Eq("Firstname", username);
            
                var userdata = await _usercollection.Find(filter).FirstOrDefaultAsync(); //integrate mongodb
                if (userdata == null)
                {
                    return NotFound("user not found");
                }
               


                return Ok(userdata.ToJson());

               
        }


    }
  
        


        
} 
    


