using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<DataContext, DataContext>();
    services.AddScoped<Authenitcation, Authenitcation>();
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngular", policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Allow only Angular app
                .AllowAnyMethod() // Allow GET, POST, PUT, DELETE, etc.
                .AllowAnyHeader() // Allow any headers
                .AllowCredentials(); // Allow cookies/auth headers
        });
    });
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();
    app.UseCors("AllowAngular");

    app.MapControllers();
    app.Run();
}
