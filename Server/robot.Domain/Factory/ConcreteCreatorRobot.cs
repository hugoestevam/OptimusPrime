namespace robot.Domain.Factory
{
    /// <summary>
    ///Classe que define o estado padrão de criação do Robo
    /// </summary>
    public class ConcreteCreatorRobot : Creator
    {
        public override Robot MakeARobot()
        {
            return new DefaultRobot();
        }

        /// <summary>
        /// Classe que representa o Robo concreto no seu estado
        /// padrão de criação
        /// </summary>
        private class DefaultRobot : Robot
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
