namespace robot.Domain.Features.Robo
{
    /// <summary>
    /// Classe abstrata que define 
    /// como será invocada a criação do Robo
    /// </summary>
    public abstract class AbstractRobotFactory
    {
        public abstract RobotAgreggate MakeARobot();
    }
}
