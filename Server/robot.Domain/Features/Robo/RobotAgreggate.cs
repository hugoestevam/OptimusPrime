using robot.Domain.Results;
using System;
using robot.Domain.Exceptions;

namespace robot.Domain.Features.Robo
{
    public abstract class RobotAgreggate : AggregateRoot
    {
        protected RobotAgreggate() { }

        public Align HeadAlign => Head.Align;
        public int HeadDirection => Head.Direction;
        public int LeftElbowPosition => LeftElbow.Position;
        public int RightElbowPosition => RightElbow.Position;
        public int LeftWristDirection => LeftWrist.Direction;
        public int RightWristDirection => RightWrist.Direction;

        public string RobotId { get; set; }
        public string RobotName { get; set; }
        protected virtual Head Head { get; set; }
        protected virtual Elbow LeftElbow { get; set; }        
        protected virtual Elbow RightElbow { get; set; }
        protected virtual Wrist LeftWrist { get; set; }
        protected virtual Wrist RightWrist { get; set; }

        /// <summary>
        /// Método responsável chamar o movimento da cabeça para cima
        /// </summary>
        public Result<Exception, Align> MoveHeadForUp()
        {
            return Head.MoveForUp();
        }

        /// <summary>
        /// Método responsável chamar o movimento da cabeça para baixo
        /// </summary>
        public Result<Exception, Align> MoveHeadToBelow()
        {
            return Head.MoveToBelow();
        }        

        /// <summary>
        /// Método responsável chamar a rotação da cabeça para a esquerda
        /// </summary>        
        public Result<Exception, int> RotateHeadToTheLeft()
        {
            if (!CanHeadRotate())
            {
                Raise(new DeniedHeadRotateEvent(Head, "Left"));
                return new DeniedHeadRotateException();
            }

            return Head.RotateToTheLeft();
        }

        /// <summary>
        /// Método responsável chamar a rotação da cabeça para a direita
        /// </summary>        
        public Result<Exception, int> RotateHeadToTheRight()
        {
            if (!CanHeadRotate())
            {
                Raise(new DeniedHeadRotateEvent(Head, "Right"));
                return new DeniedHeadRotateException();
            }

            return Head.RotateToTheRight();
        }

        /// <summary>
        /// Método responsável por chamar a contração do cotovelo esquerdo
        /// </summary>
        public Result<Exception, int> LeftElbowCollapse()
        {
            return LeftElbow.Collapse();
        }

        /// <summary>
        /// Método responsável por chamar a contração do cotovelo direito
        /// </summary>
        public Result<Exception, int> RightElbowCollapse()
        {
            return RightElbow.Collapse();
        }

        /// <summary>
        /// Método responsável por chamar a extensão do cotovelo esquerdo
        /// </summary>
        public Result<Exception, int> LeftElbowExpand()
        {
            if (LeftElbowPosition == 45 && LeftWristDirection != 0)
                LeftWrist.SetToInitialState();
                
            return LeftElbow.Expand();
        }

        /// <summary>
        /// Método responsável por chamar a extensão do cotovelo direito
        /// </summary>
        public Result<Exception, int> RightElbowExpand()
        {
            if (RightElbowPosition == 45 && RightWristDirection != 0)
                RightWrist.SetToInitialState();

            return RightElbow.Expand();
        }

        /// <summary>
        /// Método responsável por chamar a rotação a esquerda do punho esquerdo
        /// </summary>
        public Result<Exception, int> LeftWristRotateToTheLeft()
        {
            if (!CanWristRotate(LeftElbow))
                return new DeniedWristRotateException();

            return LeftWrist.RotateToTheLeft();
        }        

        /// <summary>
        /// Método responsável por chamar a rotação a direita do punho esquerdo
        /// </summary>
        public Result<Exception, int> LeftWristRotateToTheRight()
        {
            if (!CanWristRotate(LeftElbow))
                return new DeniedWristRotateException();

            return LeftWrist.RotateToTheRight();
        }

        /// <summary>
        /// Método responsável por chamar a rotação a esquerda do punho direito
        /// </summary>
        public Result<Exception, int> RightWristRotateToTheLeft()
        {
            if (!CanWristRotate(RightElbow))
                return new DeniedWristRotateException();

            return RightWrist.RotateToTheLeft();
        }

        /// <summary>
        /// Método responsável por chamar a rotação a direita do punho direito
        /// </summary>
        public Result<Exception, int> RightWristRotateToTheRight()
        {
            if (!CanWristRotate(RightElbow))
                return new DeniedWristRotateException();

            return RightWrist.RotateToTheRight();
        }

        /// <summary>
        /// Valida se pode rotacionar a cabeça
        /// Uma vez que só é possivel quando a cabeça não está inclinada para baixo
        /// </summary>        
        private bool CanHeadRotate()
        {
            return !Head.IsDown;
        }

        /// <summary>
        /// Valida se pode rotacionar o pulso
        /// Uma vez que só é possivel quando o cotovelo está fortemente contraido
        /// </summary>        
        private bool CanWristRotate(Elbow elbow)
        {
            return elbow.IsCollapsed;
        }
    }
}