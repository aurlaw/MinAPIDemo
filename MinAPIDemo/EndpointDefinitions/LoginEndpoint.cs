using MinAPIDemo.Common;
using MinAPIDemo.Models.Security;
using MinAPIDemo.Services;
using MinAPIDemo.Repositories;

namespace MinAPIDemo.EndpointDefinitions
{
    public class LoginEndpoint : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/login", async(ILoginService loginService, LoginRequest loginRequest) => 
            {
                var result = await loginService.LoginAsync(loginRequest);
                if(result.Item1)
                {
                    var jwtResponse = await loginService.GenerateTokenAsync(result.Item2!);
                    return Results.Ok(jwtResponse);
                } 
                else 
                {
                    return Results.BadRequest("Username and/or password is invalid");
                }
            });
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}