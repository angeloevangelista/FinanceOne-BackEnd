using FinanceOne.DataAccess.Contexts;
using FinanceOne.Domain.Providers;
using FinanceOne.Domain.Services;
using FinanceOne.Implementation.Providers;
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
      services.AddScoped<ISessionService, SessionService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IFinancialMovementService, FinancialMovementService>();

      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
      services.AddScoped<IFinancialMovementRepository, FinancialMovementRepository>();

      services.AddScoped<IStorageProvider, FirebaseStorageProvider>();

      /*EF Core*/
      services.AddDbContext<FinanceOneDataContext>(options =>
        options.UseNpgsql(
          Configuration.GetConnectionString("PostgreSQL"),
          action => action.MigrationsAssembly("FinanceOne.DataAccess")
        )
      );

      services.AddScoped<FinanceOneDataContext>();
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
