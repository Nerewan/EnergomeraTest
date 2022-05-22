using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task3.Domain.ItemDomain;
using Task3.Repository;

namespace Task3.Console.Registrations
{
    public static partial class RegistrationExtentions
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //var relatedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var connString = configuration.GetConnectionString("DefaultConnection");

            return services
                .AddAutoMapper(typeof(ItemMappings))
                .AddEntityFrameworkSqlite().AddDbContext<AppDbContext>(o => o.UseSqlite(connString));
        }
    }
}
