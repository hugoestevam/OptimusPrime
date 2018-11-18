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

        /// <summary>
        /// Classe que representa o Robo concreto no seu estado
        /// padrão de criação
        /// </summary>
        private class DefaultRobot : RobotAgreggate
        {
            public DefaultRobot()
            {                
                Head = new Head(Align.Normal, 0);
                LeftElbow = new Elbow(180);                
                RightElbow = new Elbow(180);
                LeftWrist = new Wrist(0);
                RightWrist = new Wrist(0);
            }
        }
    }
}
