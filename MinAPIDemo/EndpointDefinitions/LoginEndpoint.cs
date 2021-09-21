using MinAPIDemo.Common;
using MinAPIDemo.Models.Security;
using MinAPIDemo.Services;

namespace MinAPIDemo.EndpointDefinitions
{
    public class LoginEndpoint : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("/login", async(ILoginService loginService, LoginRequest loginRequest) => 
            {
                if(await loginService.LoginAsync(loginRequest))
                {
                    var jwtResponse = await loginService.GenerateTokenAsync(loginRequest);
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
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}