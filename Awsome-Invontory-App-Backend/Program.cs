using Awsome_Invontory_App_Backend.Helpers;
using Awsome_Invontory_App_Backend.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
    //app.Run();
    
    RunTest();
}

void RunTest()
{
    var auth = new Authenitcation();
    
    var password = "somePassword";

    var (hash, salt) = auth.SaltAndHash(password);
    
    Console.WriteLine($"Salt: {salt}");
    Console.WriteLine($"Hash: {hash}");
    
    var result = auth.VerifyPassword(password, hash, salt);

    Console.WriteLine(result ? "Password verification succeeded":"Password verification failed");
}