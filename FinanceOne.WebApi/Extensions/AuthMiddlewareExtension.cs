using System.Text;
using FinanceOne.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FinanceOne.WebApi.Extensions
{
  public static class AuthMiddlewareExtension
  {
    public static IServiceCollection AddAuthMiddleware(
      this IServiceCollection services,
      IConfiguration configuration
    )
    {
      var appSecret = configuration
         .GetSection("Security")
         .GetSection("AppSecret")
         .Value;

      var encodedAppSecret = Encoding.ASCII.GetBytes(appSecret);

      services.AddAuthentication(p =>
      {
        p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(p =>
      {
        p.RequireHttpsMetadata = true;
        p.SaveToken = true;

        p.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(encodedAppSecret),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });

      return services;
    }

    public static void UseAuthMiddleware(this IApplicationBuilder app)
    {
      app.UseAuthentication();
    }
  }
}
