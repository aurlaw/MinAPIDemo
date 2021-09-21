using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinAPIDemo.Models;
using MinAPIDemo.Models.Security;

namespace MinAPIDemo.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtTokenOptions _options;
        private readonly SigningCredentials _signingCredentials;
        private readonly TimeSpan _tokenExpiration;
        
        public LoginService(IOptions<JwtTokenOptions> options)
        {
            _options = options.Value;
            _signingCredentials = new SigningCredentials(JwtTokenOptions.CreateKey(_options.SecretKey), 
                SecurityAlgorithms.HmacSha256);
            _tokenExpiration = TimeSpan.FromMinutes(_options.TokenExpiryMins);
        }

        public Task<LoginResponse> GenerateTokenAsync(LoginRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            var newTokenExpiration = DateTime.UtcNow.Add(_tokenExpiration);
            var identity = new ClaimsIdentity(
                new GenericIdentity(request.Username, "TokenAuth"),
                new[] {new Claim("ID", Guid.NewGuid().ToString())}
            );
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _options.Issuer,
                    Audience = _options.Audience,
                    SigningCredentials = _signingCredentials,
                    Subject = identity,
                    Expires = newTokenExpiration
                }
            );
            var encodedToken = handler.WriteToken(securityToken);
            // Update token data on database: TODO
            var response = new LoginResponse
            {
                Username = request.Username,
                Token = encodedToken,
                Expiration = (int)_tokenExpiration.TotalSeconds
            };
            return Task.FromResult(response);
        }

        public Task<bool> LoginAsync(LoginRequest request)
        {
            return Task.FromResult(
                request.Username == "username" && request.Password == "password"
            );
        }
    }
}