using hexagonal.application.external;
using hexagonal.application.services;
using hexagonal.application.services.Cuenta;
using hexagonal.application.services.genero.interfaces;
using hexagonal.application.services.Genero;
using hexagonal.application.services.log.interfaces;
using hexagonal.application.services.log;
using hexagonal.application.services.registroPersona;
using hexagonal.application.services.registroPersona.interfaces;
using hexagonal.application.services.usuario.interfaces;
using hexagonal.application.services.Usuario;
using hexagonal.data.contexts;
using hexagonal.data.repositories.generics;
using hexagonal.domain.repositories.interfaces;
using hexagonal.domain.repositories.interfaces.generics;
using hexagonal.infrastructure.data.repositories.genero;
using hexagonal.infrastructure.data.repositories.usuario;
using hexagonal.infrastructure.data.repositories.log;

namespace hexagonal.api.extensions.injections
{
    public class DependencyInjectionExtension
    {
        public static void ConfigureDependenciesInjectionsServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<DataDbContext>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<RegistroPersonaClient>();
            builder.Services.AddScoped<TramiteClient>();
            builder.Services.AddScoped<CorreoClient>();
            builder.Services.AddScoped<IRegistroPersonaService, RegistroPersonaService>();


            builder.Services.AddScoped<ILogDomainRepository, LogRepository>();
            builder.Services.AddScoped<ILogService, LogService>();

            //add genero
            builder.Services.AddScoped<IGeneroDomainRepository, GeneroRepository>();
            builder.Services.AddScoped<IGeneroService, GeneroService>();

            //add usuario
            builder.Services.AddScoped<IUsuarioDomainRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();

            //add cuenta
            builder.Services.AddScoped<ICuentaDomainRepository, CuentaRepository>();
            builder.Services.AddScoped<ICuentaService, CuentaService>();
        }
    }
}