using System;
using robot.Domain.Exceptions;
using robot.Domain.Results;

namespace robot.Domain.Features.Robo
{
    /// <summary>
    /// Essa classe representa o status de cada cotovelo do robo
    /// Para expansão e contração, optou-se por usar angulos de 45° até 180°,
    /// sempre com incremento e decremento de 45°,
    /// possibilitando no futuro uma maior mobilidade desse movimento
    /// </summary>
    public class Elbow
    {
        private readonly int AngleRotate = 45;
        private readonly int LimitElbowCollapse = 45;
        private readonly int LimitElbowExpand = 180;

        public Elbow(int position)
        {
            Position = position;
        }

        public int Position { get; private set; }

        public bool IsCollapsed => Position == LimitElbowCollapse;

        /// <summary>
        /// Método responsável por contrair o cotovelo
        /// </summary>
        public Result<Exception, int> Collapse()
        {
            if (Position == LimitElbowCollapse)
                return new LimitedElbowCollapseException();

            Position = Position - AngleRotate;
            return Position;
        }

        /// <summary>
        /// Método responsável por estender o cotovelo
        /// </summary>
        public Result<Exception, int> Expand()
        {
            if (Position == LimitElbowExpand)
                return new LimitedElbowExpandException();

            Position = Position + AngleRotate;
            return Position;
        }
    }
}