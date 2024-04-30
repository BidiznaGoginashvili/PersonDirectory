using PersonDirectory.Shared.DomainModels;
using Microsoft.Extensions.DependencyInjection;

namespace PersonDirectory.Shared.Dispatcher
{
    public class EventDispatcher : IEventDispatcher<DomainEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider) =>
            _serviceProvider = serviceProvider;

        public async Task DispatchAsync(IReadOnlyList<DomainEvent> events, CancellationToken cancellationToken)
        {
            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                var handlerType = typeof(IHandleEvent<>).MakeGenericType(eventType);
                var handlers = _serviceProvider.GetServices(handlerType);

                foreach (dynamic handler in handlers)
                {
                    await handler.HandleAsync((dynamic)@event, cancellationToken);
                }
            }
        }
    }
}
