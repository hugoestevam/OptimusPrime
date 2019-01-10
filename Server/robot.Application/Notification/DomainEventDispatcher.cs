using MediatR;
using robot.Domain;
using System.Collections.Generic;

namespace robot.Application.Notification
{
    /// <summary>
    /// Classe utilizada para estender o comportamento do Mediator
    /// publicando um evento de dominio genérico atráves de um Adapter
    /// </summary>
    public static class DomainEventDispatcher
    {    
        public static void PublishDomainEvents(this IMediator _mediator, IEnumerable<IEvent> events)
        {
            if (events == null)
                return;

            foreach (var @event in events)
            {
                var domainEventNotification = CreateDomainEventNotification((dynamic)@event);

                _mediator.Publish(domainEventNotification);
            }
        }

        private static DomainEventAdapter<TDomainEvent> CreateDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IEvent
        {
            return new DomainEventAdapter<TDomainEvent>(domainEvent);
        }
    }
}
