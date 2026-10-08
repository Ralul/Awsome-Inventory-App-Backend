using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddControllers();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<DataContext, DataContext>();
    services.AddScoped<Authenitcation, Authenitcation>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
    app.Run();
}
