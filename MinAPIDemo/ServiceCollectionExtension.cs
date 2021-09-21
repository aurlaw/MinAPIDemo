using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinAPIDemo.Models;

namespace MinAPIDemo
{
    public static class ServiceCollectionExtension
    {

         // Configure authentication with JWT (Json Web Token).
        public static void AddJwtAuthService(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var options = configurationManager.GetSection("JwtToken");
            //Action<JwtTokenOptions> configureOptions
            services.Configure<JwtTokenOptions>(options.Bind);
            // Enable the use of an [Authorize(AuthenticationSchemes = 
            // JwtBearerDefaults.AuthenticationScheme)]
            // attribute on methods and classes to protect.
            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = true;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = JwtTokenOptions.CreateKey(options["SecretKey"]),
                    ValidAudience = options["Audience"],
                    ValidIssuer = options["Issuer"],
                    // When receiving a token, check that we've signed it.
                    ValidateIssuerSigningKey = true,
                    // When receiving a token, check that it is still valid.
                    ValidateLifetime = true,
                    // This defines the maximum allowable clock skew when validating 
                    // the lifetime. As we're creating the tokens locally and validating
                    // them on the same machines which should have synchronised time,
                    // this can be set to zero.
                    ClockSkew = TimeSpan.FromMinutes(0)                    
                };
            });

        }        
    }
}
