using NUnit.Framework;
using robot.Domain.Exceptions;
using robot.Domain.Results;
using robot.Domain.Features.Robo;
using robot.Domain.Test.Initialize;
using Shouldly;
using System.Threading.Tasks;

namespace robot.Infra.Data.Test
{
    public class RobotRepositoryTest
    {
        AbstractRobotFactory creator;
        IRobotRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new RobotRepository();
        }

        [Test]
        public async Task RepositoryAddRobotTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            RobotAgreggate robot = creator.MakeARobot();

            //Action
            var result = await repository.Add(robot);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.RobotId.ShouldNotBe(0);
        }

        [Test]
        public async Task RepositoryGetAllRobotTest()
        {
            //Action
            var result = await repository.GetAll();

            result.IsSuccess.ShouldBeTrue();
            result.Value.Count.ShouldBe(1);
        }

        [Test]
        public async Task RepositoryGetByIdRobotTest()
        {
            //Action
            var result = await repository.Get(0991532625149);

            result.IsSuccess.ShouldBeTrue();
            result.Value.RobotId.ShouldBe(0991532625149);
        }

        [Test]
        public async Task RepositoryUpdateRobotTest()
        {
            //Arrange
            var robot = await repository.Get(0991532625149);

            //Action
            robot.Value.MoveHeadForUp();
            var result = await repository.Update(robot.Value);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.HeadAlign.ShouldBe(Align.Top);
        }

        [Test]
        public async Task RepositoryDeleteRobotTest()
        {
            //Action
            var result = await repository.Delete(0991532625149);
            var resultGet = await repository.Get(0991532625149);

            //Assert
            result.IsSuccess.ShouldBeTrue();             
            resultGet.IsSuccess.ShouldBeFalse();
            resultGet.Error.ShouldBeOfType<NotFoundException>();
        }
    }
}
