using Movies.Infrastructure.Data;

namespace Movies.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);
    
        var app = builder.Build();
        startup.Configure(app, builder.Environment);
        //app.MapGet("/", () => "Hello World!");

        await SeedDBData(app);    
        app.Run();

        async Task SeedDBData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var context = services.GetRequiredService<MovieContext>();
                try
                {
                    await MovieContextSeed.SeedAsync(context, loggerFactory);
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "An error occurred seeding the DB.");
                }
            }
        }
    }
}
