namespace Web.Users.Models.RequestModels
{
    public class UserSignUpRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    }
}