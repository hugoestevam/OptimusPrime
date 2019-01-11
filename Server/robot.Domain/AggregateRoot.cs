using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Domain
{
    /// <summary>
    /// Classe abstrata que define o comportamento
    /// de disparo de eventos do dominio.
    /// A ideia é que os eventos de domínio não seja disparados imediatamente:
    /// fonte: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/
    /// </summary>
    public abstract class AggregateRoot
    {
        private List<IEvent> events;

        public void Raise(IEvent @event)
        {
            if (events == null)
            {
                events = new List<IEvent>();
            }

            events.Add(@event);
        }

        public virtual IEnumerable<IEvent> RaisedEvents() => events;
    }
}
