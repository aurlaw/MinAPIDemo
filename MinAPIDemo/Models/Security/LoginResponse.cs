namespace MinAPIDemo.Models.Security
{
    public class LoginResponse
    {
        public string Username {get;set;} = default!;
        public string Token {get;set;} = default!;
        public int Expiration {get;set;}
        
    }
}