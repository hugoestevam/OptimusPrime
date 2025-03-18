using Moq;
using robot.Domain.Features.Robo;

namespace robot.Domain.Test.Initialize
{
    /// <summary>
    /// Classe que Imita a criação do Robo
    /// Essa classe é utilizada somente para testes
    /// </summary>
    public class MockCreatorRobot : AbstractRobotFactory
    {
        public override RobotAgreggate MakeARobot()
        {
            return new MockRobot();
        }

        public override RobotAgreggate MountARobot(long id, string name, int status, int align, int direction, int leftElbow, int rightElbow, int leftWrist, int rightWrist)
        {
            throw new System.NotImplementedException();
        }
    }
}
