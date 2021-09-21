using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MinAPIDemo.Models
{
    public class JwtTokenOptions
    {
        public string SecretKey {get;set;} = default!;
        public string Issuer {get;set;} = default!;
        public string Audience {get;set;} = default!;
        public int TokenExpiryMins {get;set;}

        public static SymmetricSecurityKey CreateKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }
    }
}