namespace MinAPIDemo.Models.Domain
{
    public class UserEntity
    {
        public string Id {get;set;} = default!;
        public string Username {get;init;} = default!;
        public string Password {get;init;} = default!;
        public string? Token {get;set;} = default!;
        public DateTime? TokenExpiration {get;set;}
        

    }
}