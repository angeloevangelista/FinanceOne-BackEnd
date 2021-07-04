using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Services;
using FinanceOne.Implementation.Repositories;
using FinanceOne.Implementation.Services;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Shared.Repositories;
using FinanceOne.Shared.Services;
using FinanceOne.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FinanceOne.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
      {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
      };

      services.AddGlobalExceptionHandlerMiddleware();

      services.AddScoped<IJwtService, JwtService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IHashService, HashService>();
      services.AddScoped<IUserRepository, UserRepository>(); // Singleton it's beeing used for tests
      services.AddScoped<ISessionService, SessionService>();

      /*EF Core*/
      services.AddDbContext<UserDataContext>(options =>
        options.UseNpgsql(
          Configuration.GetConnectionString("PostgreSQL"),
          action => action.MigrationsAssembly("FinanceOne.WebApi")
        )
      );

      services.AddScoped<UserDataContext>();
      /*EF Core*/

      services.AddCors();
      services.AddControllers();
      services.AddAuthMiddleware(this.Configuration);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinanceOne.WebApi", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanceOne.WebApi v1"));
      }

      app.UseHttpsRedirection();
      app.UseGlobalExceptionHandlerMiddleware();
      app.UseRouting();

      app.UseCors(p => p
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
      );

      app.UseAuthMiddleware(); // AuthMiddleware
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
