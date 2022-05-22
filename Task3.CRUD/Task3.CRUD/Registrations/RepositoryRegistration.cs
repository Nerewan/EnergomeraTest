using Microsoft.Extensions.DependencyInjection;
using Task3.Repository.ItemRepository;

namespace Task3.Console.Registrations
{
    public static partial class RegistrationExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IItemRepository, ItemRepository>();
        }
    }
}
