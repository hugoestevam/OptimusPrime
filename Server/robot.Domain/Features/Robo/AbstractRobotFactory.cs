namespace robot.Domain.Features.Robo
{
    /// <summary>
    /// Classe abstrata que define 
    /// como será invocada a criação do Robo
    /// </summary>
    public abstract class AbstractRobotFactory
    {
        public abstract RobotAgreggate MakeARobot();
        public abstract RobotAgreggate MountARobot(long id, string name, int status, int align, int direction, int leftElbow, int rightElbow, int leftWrist, int rightWrist);
    }
}
