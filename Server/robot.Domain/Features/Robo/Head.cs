using robot.Domain.Results;
using System;
using robot.Domain.Exceptions;

namespace robot.Domain.Features.Robo
{
    public enum Align : int
    {
        Botton,
        Normal,
        Top
    }

    /// <summary>
    /// Essa classe representa o status da cabeça do robo
    /// Para alinhamento optou-se por usar um enum
    /// Para direção optou-se por usar um int, possibilitanto maior grau de 
    /// mobilidade no futuro, apenas diminuindo o angulo de rotação a cada movimento
    /// </summary>
    public class Head
    {
        private readonly int AngleRotate = 45;
        private readonly int LeftLimit = 90;
        private readonly int RightLimit = -90;

        public Head(Align normal, int direction)
        {
            Align = normal;
            Direction = direction;
        }

        public Align Align { get; private set; }
        public int Direction { get; private set; }
        public bool IsDown => Align == Align.Botton;

        /// <summary>
        /// Método responsável por movimentar o alinhamento da cabeça para cima
        /// </summary>
        /// <returns></returns>
        public Result<Align> MoveForUp()
        {
            if (Align == Align.Top)
                return Result<Align>.Fail(new LimitedHeadMoveException());

            Align = Align + 1;
            return Align;
        }

        /// <summary>
        /// Método responsável por movimentar o alinhamento da cabeça para baixo
        /// </summary>
        /// <returns></returns>
        public Result<Align> MoveToBelow()
        {
            if (Align == Align.Botton)
                return Result<Align>.Fail(new LimitedHeadMoveException());

            Align = Align - 1;
            return Align;
        }

        /// <summary>
        /// Método responsável por rotacionar a cabeça para a esquerda
        /// </summary>
        /// <returns></returns>
        public Result<int> RotateToTheLeft()
        {
            if (Direction == LeftLimit)
                return Result<int>.Fail(new LimitedHeadRotateException());

            Direction = Direction + AngleRotate;
            return Direction;
        }

        /// <summary>
        /// Método responsável por rotacionar a cabeça para a direita
        /// </summary>
        /// <returns></returns>
        public Result<int> RotateToTheRight()
        {
            if (Direction == RightLimit)
                return Result<int>.Fail(new LimitedHeadRotateException());

            Direction = Direction - AngleRotate;
            return Direction;
        }
    }
}