using hexagonal.application.services;
using hexagonal.application.services.person;
using hexagonal.application.services.person.interfaces;
using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.data.repositories.person;
using hexagonal.domain.repositories.interfaces;
using hexagonal.domain.repositories.interfaces.generics;

namespace $safeprojectname$.extensions.injections
{
  public class DependencyInjectionExtension
  {
    public static void ConfigureDependenciesInjectionsServices(WebApplicationBuilder builder)
    {

      builder.Services.AddScoped<DataDbContext>();
      builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
      builder.Services.AddScoped<IPersonDomainRepository, PersonRepository>();
      
      //builder.Services.AddScoped<IPersonService, PersonService>();
    }
  }
}