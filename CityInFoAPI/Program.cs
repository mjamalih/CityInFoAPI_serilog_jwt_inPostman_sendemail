using CityInfo.API.Services;
using CityInFoAPI;
using CityInFoAPI.DbContexts;
using CityInFoAPI.Repositoties;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();


builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddDbContext<CityInfoDbContext>(option =>
{

    //option.UseSqlite("Data Source=CityInfo.db");
    option.UseSqlite(builder.Configuration["ConnectionStrings:cityConnectionString"]);
    //option.UseSqlite("Data Source=192.168.1.1;Initial  Catalog=ImanDb;User ID=iman;Password=123");
});

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped<IMailService,LocalMailService>();
#if DEBUG
        builder.Services.AddScoped<IMailService, LocalMailService>();
#else
        builder.Services.AddScoped<IMailService,LocalMailService>();
#endif

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
                )
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//  Controller/Action/id?

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});





// Page Not Found
app.Run(async (context) =>
    await context.Response.WriteAsJsonAsync("Not Found")
    );
app.Run();
