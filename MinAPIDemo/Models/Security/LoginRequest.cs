namespace MinAPIDemo.Models.Security
{
    public record LoginRequest
    {
        public string Username {get;init;} = default!;
        public string Password {get;init;} = default!;
    }
}