using System;
using robot.Domain.Exceptions;
using robot.Domain.Results;

namespace robot.Domain.Features.Robo
{
    /// <summary>
    /// Essa classe representa o status de cada pulso
    /// Para direção optou-se por usar um int, possibilitanto maior grau de 
    /// mobilidade no futuro, apenas diminuindo o angulo de rotação a cada movimento
    /// </summary>
    public class Wrist
    {
        private readonly int AngleRotate = 45;
        private readonly int LeftLimit = 180;
        private readonly int RightLimit = -90;

        public Wrist(int direction)
        {
            Direction = direction;
        }

        public int Direction { get; private set; }

        /// <summary>
        /// Método responsável por rotacionar o punho para a esquerda
        /// </summary>
        public Result<Exception, int> RotateToTheLeft()
        {
            if (Direction == LeftLimit)
                return new LimitedWristRotateException();

            Direction = Direction + AngleRotate;
            return Direction;
        }

        /// <summary>
        /// Método responsável por rotacionar o punho para a direita
        /// </summary>
        public Result<Exception, int> RotateToTheRight()
        {
            if (Direction == RightLimit)
                return new LimitedWristRotateException();

            Direction = Direction - AngleRotate;
            return Direction;
        }

        /// <summary>
        /// Método responsável voltar o pulso para o estado inicial
        /// </summary>
        public void SetToInitialState()
        {
            Direction = 0;
        }
    }
}