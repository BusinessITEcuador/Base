using hexagonal.api.extensions.automappers;
using hexagonal.api.extensions.injections;
using hexagonal.api.extensions.servers;
using hexagonal.infrastructure.api.models;
using Microsoft.Identity.Web;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAdB2C");

builder.Services.AddAuthentication("JwtBearerAuth")
    .AddJwtBearer("JwtBearerAuth", options =>
    {
        options.Authority = builder.Configuration["AuthServer:Authority"];
        options.Audience = builder.Configuration["AuthServer:Audience"];
    });

builder.Services.Configure<GraphApiOptions>(builder.Configuration.GetSection("GraphApi"));

builder.Services.AddAuthorization();

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
              .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
              .Build();

ServerExtension.ConfigureSQLServices(builder);
DependencyInjectionExtension.ConfigureDependenciesInjectionsServices(builder);
AutoMapperExtension.ConfigureAutoMappersServices(builder.Services);
// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();