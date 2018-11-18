using NUnit.Framework;
using robot.Domain.Exceptions;
using robot.Domain.Results;
using robot.Domain.Features.Robo;
using robot.Domain.Test.Initialize;
using Shouldly;

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
        public void RepositoryAddRobotTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            RobotAgreggate robot = creator.MakeARobot();

            //Action
            var result = repository.Add(robot);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Success.RobotId.ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void RepositoryGetAllRobotTest()
        {
            //Action
            var result = repository.GetAll();

            result.IsSuccess.ShouldBeTrue();
            result.Success.Count.ShouldBe(1);
        }

        [Test]
        public void RepositoryGetByIdRobotTest()
        {
            //Action
            var result = repository.Get("099153c2625149bc8ecb3e85e03f0022");

            result.IsSuccess.ShouldBeTrue();
            result.Success.RobotId.ShouldBe("099153c2625149bc8ecb3e85e03f0022");
        }

        [Test]
        public void RepositoryUpdateRobotTest()
        {
            //Arrange
            RobotAgreggate robot = null;
            robot = repository.Get("099153c2625149bc8ecb3e85e03f0022").Success;

            //Action
            robot.MoveHeadForUp();
            var result = repository.Update(robot);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Success.HeadAlign.ShouldBe(Align.Top);
        }

        [Test]
        public void RepositoryDeleteRobotTest()
        {
            //Action
            var result = repository.Delete("099153c2625149bc8ecb3e85e03f0022");
            var resultGet = repository.Get("099153c2625149bc8ecb3e85e03f0022");

            //Assert
            result.IsSuccess.ShouldBeTrue();            
            resultGet.IsFailure.ShouldBeTrue();
            resultGet.Failure.ShouldBeOfType<NotFoundException>();
        }
    }
}
