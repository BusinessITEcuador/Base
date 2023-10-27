using hexagonal.data.contexts;
using Microsoft.EntityFrameworkCore;

namespace hexagonal.api.extensions.servers
{
    public class ServerExtension
    {
        public static void ConfigureSQLServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataDbContext>(
              options =>
              {
                  options.UseSqlServer(builder.Configuration.GetConnectionString("TenantPrincipal"));
                  //options.UseInMemoryDatabase("TenantPrincipal");
              });
        }
    }
}