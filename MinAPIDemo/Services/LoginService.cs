using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinAPIDemo.Models;
using MinAPIDemo.Models.Domain;
using MinAPIDemo.Models.Security;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.Services
{
    public class LoginService : ILoginService
    {
        private readonly JwtTokenOptions _options;
        private readonly SigningCredentials _signingCredentials;
        private readonly TimeSpan _tokenExpiration;
        private readonly IUserRepository _userRepository;
        public LoginService(IOptions<JwtTokenOptions> options, IUserRepository userRepository)
        {
            _options = options.Value;
            _signingCredentials = new SigningCredentials(JwtTokenOptions.CreateKey(_options.SecretKey),
                SecurityAlgorithms.HmacSha256);
            _tokenExpiration = TimeSpan.FromMinutes(_options.TokenExpiryMins);
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> GenerateTokenAsync(UserEntity user)
        {
            var handler = new JwtSecurityTokenHandler();
            var newTokenExpiration = DateTime.UtcNow.Add(_tokenExpiration);
            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Username, "TokenAuth"),
                new[] {new Claim("ID", user.Id)}
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
            // Update token data on database
            await _userRepository.UpdateUserToken(user.Id, encodedToken, newTokenExpiration);
            var response = new LoginResponse
            {
                Username = user.Username,
                Token = encodedToken,
                Expiration = (int)_tokenExpiration.TotalSeconds
            };
            return response;
        }

        public async Task<Tuple<bool, UserEntity?>>  LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByLoginAsync(request.Username, request.Password);
            return new Tuple<bool, UserEntity?>(user is not null, user);
        }
    }
}