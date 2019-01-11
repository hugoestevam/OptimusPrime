using MediatR;
using robot.Application.Notification;
using robot.Domain.Features.Robo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace robot.Application.Features.Robo.Handlers
{
    /// <summary>
    /// Handler que implementa o disparo do evento 
    /// </summary>
    public class HeadRotateNotificationHandler : INotificationHandler<DomainEventAdapter<DeniedHeadRotateEvent>>
    {

        public HeadRotateNotificationHandler()
        {

        }

        public Task Handle(DomainEventAdapter<DeniedHeadRotateEvent> notification, CancellationToken cancellationToken)
        {
            //TODO: Inventar alguma coisa funcional
            return Task.Run(() =>
                    Debug.WriteLine($"Evento de Rotação Limitada para a {notification.DomainEvent._direction}"));
        }
    }
}
