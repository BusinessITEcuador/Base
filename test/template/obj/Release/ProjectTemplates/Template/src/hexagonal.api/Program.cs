using $safeprojectname$.extensions.automappers;
using $safeprojectname$.extensions.injections;
using $safeprojectname$.extensions.servers;
using System.Net;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
  builder
  .AllowAnyMethod()
  .AllowAnyHeader()
  .AllowCredentials()
  .WithOrigins("http://localhost:4200");
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
//AutoMapperExtension.ConfigureAutoMappersServices(builder.Services);
// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
