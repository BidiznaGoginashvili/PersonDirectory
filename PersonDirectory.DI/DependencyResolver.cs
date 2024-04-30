using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Dispatcher;
using Microsoft.Extensions.Configuration;
using PersonDirectory.Shared.DomainModels;
using PersonDirectory.Shared.Infrastructure;
using PersonDirectory.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Application.EventHandlers;
using PersonDirectory.Infrastructure.Repositories;
using PersonDirectory.Domain.CityManagement.Repository;
using PersonDirectory.Application.Commands.CreatePerson;
using PersonDirectory.Domain.PersonManagement.Repository;
using PersonDirectory.Domain.PersonManagement.DomainEvents;

namespace PersonDirectory.DI
{
    public class DependencyResolver
    {
        private IConfiguration _configuration { get; }

        public DependencyResolver(IConfiguration configuration) =>
            _configuration = configuration;

        public IServiceCollection Resolve(IServiceCollection services)
        {
            services ??= new ServiceCollection();

            var connectionString = _configuration.GetConnectionString(nameof(DatabaseContext));

            services.AddDbContext<DatabaseContext>(options =>
                                                   options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IEventDispatcher<DomainEvent>, EventDispatcher>();

            services.AddApplicationPipelines(typeof(CreatePersonCommand).Assembly);
            services.AddScoped<IHandleEvent<PersonCreatedDomainEvent>, PersonEventHandlers>();
            services.AddScoped<IHandleEvent<PersonChangedDomainEvent>, PersonEventHandlers>();
            services.AddScoped<IHandleEvent<PersonDeletedDomainEvent>, PersonEventHandlers>();

            services.AddLocalization(options =>
            {
                options.ResourcesPath = "PersonDirectory.Application.Resources";
            });

            return services;
        }
    }
}
