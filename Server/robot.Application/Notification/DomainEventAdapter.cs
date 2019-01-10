using MediatR;
using robot.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Application.Notification
{ 
    /// <summary>
    /// Classe utilizada para adaptar o domain event para ser processado
    /// pelo Mediator, seguindo a padrão de usar INotification
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public class DomainEventAdapter<TDomainEvent> : INotification where TDomainEvent : IEvent
    {
        public TDomainEvent DomainEvent { get; }

        public DomainEventAdapter(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }

}
