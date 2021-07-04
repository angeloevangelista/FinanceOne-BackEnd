using FinanceOne.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceOne.WebApi.Extensions
{
  public static class GlobalExceptionHandlerMiddlewareExtension
  {
    public static IServiceCollection AddGlobalExceptionHandlerMiddleware(
      this IServiceCollection services)
    {
      return services.AddTransient<GlobalExceptionHandlerMiddleware>();
    }

    public static void UseGlobalExceptionHandlerMiddleware(
      this IApplicationBuilder app)
    {
      app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
  }
}
