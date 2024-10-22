using System.Reflection;
using API.Jobs;
using DAL.Helper;
using Messaging.Abstraction.IConsumerService;
using Messaging.ConsumerService;
using Messaging.ProducerService;
using Service.Helper;

namespace API.ServiceRegister
{
    public static class ServiceRegister
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddHostedService<BackgroundWorker>();
            services.AddScoped<ISysGeneralCodeProducerService, SysGeneralCodeProducerService>();
            services.AddScoped<ISysGeneralCodeConsumerService, SysGeneralCodeConsumerService>();
            // services.AddScoped<IMasterBookRepository, MasterBookRepository>();
            // services.AddScoped<IMasterBookService, MasterBookService>();

            List<Type> allRepositories = Assembly.Load("DAL")
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseRepository)))
            .ToList();

            Console.WriteLine($"\nRegistering {allRepositories.Count} Repository");

            allRepositories.ForEach(repositoryClass =>
            {
                var repositoryInterface = repositoryClass.GetInterfaces().Where(i => !i.IsGenericType).First();

                Console.WriteLine($"Registering Repository {repositoryClass.Name} with interface {repositoryInterface.Name}");
                services.AddScoped(repositoryInterface, repositoryClass);
            });

            List<Type> allServices = Assembly.Load("Service")
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseService)))
            .ToList();

            Console.WriteLine($"\nRegistering {allServices.Count} Service");
            allServices.ForEach(serviceClass =>
            {
                var serviceInterface = serviceClass.GetInterfaces().Where(i => !i.IsGenericType).First();

                Console.WriteLine($"Registering Service {serviceClass.Name} with interface {serviceInterface.Name}");
                services.AddScoped(serviceInterface, serviceClass);
            });
        }
    }
}
