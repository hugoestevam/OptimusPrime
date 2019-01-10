using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Domain.Features.Robo
{
    /// <summary>
    /// Classe que representa um evento de dominio
    /// Quando o robo tentou rotacionar a cabeça
    /// </summary>
    public class DeniedHeadRotateEvent : IEvent
    {
        public DeniedHeadRotateEvent(Head head, string direction)
        {
            _head = head;
            _direction = direction;
        }

        public Head _head { get; private set; }
        public string _direction { get; private set; }
    }
}
