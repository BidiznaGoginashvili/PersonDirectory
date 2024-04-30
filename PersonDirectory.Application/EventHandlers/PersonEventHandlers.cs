using Microsoft.EntityFrameworkCore;
using PersonDirectory.Shared.Dispatcher;
using PersonDirectory.Domain.CityManagement;
using PersonDirectory.Infrastructure.Database;
using PersonDirectory.Domain.PersonManagement.ReadModels;
using PersonDirectory.Domain.PersonManagement.DomainEvents;

namespace PersonDirectory.Application.EventHandlers
{
    public class PersonEventHandlers : IHandleEvent<PersonCreatedDomainEvent>,
                                       IHandleEvent<PersonChangedDomainEvent>,
                                       IHandleEvent<PersonDeletedDomainEvent>
    {
        private readonly DatabaseContext _databaseContext;
        public PersonEventHandlers(DatabaseContext databaseContext) =>
            _databaseContext = databaseContext;

        public async Task HandleAsync(PersonCreatedDomainEvent @event, CancellationToken cancellationToken)
        {
            var city = await _databaseContext.Set<City>()
                                             .FirstOrDefaultAsync(city => city.Id == @event.Person.CityId);

            var readModel = new PersonReadModel(city == null? string.Empty : city.Name , @event.Person);

            await _databaseContext.AddAsync(readModel);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task HandleAsync(PersonChangedDomainEvent @event, CancellationToken cancellationToken)
        {
            var readModel = await _databaseContext.Set<PersonReadModel>()
                                                  .FirstOrDefaultAsync(readModel => readModel.PersonId == @event.Person.Id);

            if (readModel != null)
            {
                var city = await _databaseContext.Set<City>()
                                                 .FirstOrDefaultAsync(city => city.Id == @event.Person.CityId);

                readModel.ChangeDetails(city == null ? string.Empty : city.Name, @event.Person);

                _databaseContext.Update(readModel);
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task HandleAsync(PersonDeletedDomainEvent @event, CancellationToken cancellationToken)
        {
            var readModel = await _databaseContext.Set<PersonReadModel>()
                                                  .FirstOrDefaultAsync(readModel => readModel.PersonId == @event.Id);

            if (readModel != null)
            {
                _databaseContext.Remove(readModel);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
