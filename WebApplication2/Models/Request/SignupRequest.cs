namespace WebApplication2.Models.Request
{
    public class SignupRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Mobile { get; set; }
    }
}
