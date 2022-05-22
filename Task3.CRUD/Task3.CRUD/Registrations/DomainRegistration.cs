using Microsoft.Extensions.DependencyInjection;
using Task3.Domain.ItemDomain;

namespace Task3.Console.Registrations
{
    public static partial class RegistrationExtentions
    {
        public static IServiceCollection RegisterDomains(this IServiceCollection services)
        {
            return services
                .AddScoped<IItemDomain, ItemDomain>();
        }
    }
}
