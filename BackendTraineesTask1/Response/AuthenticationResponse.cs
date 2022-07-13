namespace BackendTraineesTask1.Response
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
       
        public string JwtToken { get; set; }
        public AuthenticationResponse(Models.User user, string jwtToken)
        {
            Id = user.Id;
            JwtToken = jwtToken;
  
        }
    }
}