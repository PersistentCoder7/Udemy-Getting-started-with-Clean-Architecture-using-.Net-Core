using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.Application.Commands;
using Movies.Core.Repositories;
using Movies.Core.Repositories.Base;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Repositories.Base;

namespace Movies.API;

public class Startup
{
    public IConfiguration Configuration { get; set; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MovieContext>(x=> x.UseSqlServer(Configuration.GetConnectionString("MovieConnection")), 
            ServiceLifetime.Singleton);
        services.AddControllers();
        services.AddApiVersioning();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movies API Review", Version = "v1" });
        });
        services.AddAutoMapper(typeof(Startup));
        services.AddMediatR(x=>
            x.RegisterServicesFromAssembly( typeof(CreateMovieCommand).GetTypeInfo().Assembly)
            );
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IMovieRepository, MovieRepository>();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseSwagger();
        app.UseSwaggerUI((x =>
        {
            x.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API Review");
            x.RoutePrefix = string.Empty;
        }));
    }
}