namespace robot.Domain.Features.Robo
{
    /// <summary>
    ///Classe que define o estado padrão de criação do Robo
    /// </summary>
    public class ConcreteRobotFactory : AbstractRobotFactory
    {
        public override RobotAgreggate MakeARobot()
        {
            return new DefaultRobot();
        }

        public override RobotAgreggate MountARobot(long id, string name, int status, int align, int direction, 
            int leftElbow, int rightElbow, int leftWrist, int rightWrist)
        {
            return new MountedRobot(
                id,
                name,
                align,
                status,
                direction,
                leftElbow,
                rightElbow,
                leftWrist,
                rightWrist
            );
        }

        /// <summary>
        /// Classe que representa o Robo concreto no seu estado
        /// padrão de criação
        /// </summary>
        private class DefaultRobot : RobotAgreggate
        {
            public DefaultRobot()
            {                
                RobotName = "Default Robot";
                Status = RobotStatus.Offline;
                Head = new Head(Align.Normal, 0);
                LeftElbow = new Elbow(180);                
                RightElbow = new Elbow(180);
                LeftWrist = new Wrist(0);
                RightWrist = new Wrist(0);
            }
        }

        /// <summary>
        /// Classe que representa o Robo concreto no seu estado
        /// </summary>
        private class MountedRobot : RobotAgreggate
        {
            public MountedRobot(long id, string name, int status, int align, int direction, 
                int leftElbow, int leftWrist, int rightElbow, int rightWrist)
            {
                RobotId = id;
                RobotName = name;
                Status = (RobotStatus)status;
                Head = new Head((Align)align, direction);
                LeftElbow = new Elbow(leftElbow);
                RightElbow = new Elbow(rightElbow);
                LeftWrist = new Wrist(leftWrist);
                RightWrist = new Wrist(rightWrist);
            }
        }
    }
}
