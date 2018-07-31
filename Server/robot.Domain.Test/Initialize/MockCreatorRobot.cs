using Moq;
using robot.Domain.Factory;

namespace robot.Domain.Test.Initialize
{
    /// <summary>
    /// Classe que Imita a criação do Robo
    /// Essa classe é utilizada somente para testes
    /// </summary>
    public class MockCreatorRobot : Creator
    {
        public override Robot MakeARobot()
        {
            return new MockRobot();
        }
    }
}
