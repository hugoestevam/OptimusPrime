using NUnit.Framework;
using robot.Domain;
using robot.Domain.Contract;
using robot.Domain.Exceptions;
using robot.Domain.Factory;
using robot.Domain.Test.Initialize;
using Shouldly;

namespace robot.Infra.Data.Test
{
    public class RobotRepositoryTest
    {
        Creator creator;
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
            Robot robot = creator.MakeARobot();

            //Action
            var result = repository.Add(robot);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.RobotId.ShouldNotBeNullOrEmpty();
        }

        [Test]
        public void RepositoryGetAllRobotTest()
        {
            //Action
            var result = repository.GetAll();

            result.IsSuccess.ShouldBeTrue();
            result.Result.Count.ShouldBe(1);
        }

        [Test]
        public void RepositoryGetByIdRobotTest()
        {
            //Action
            var result = repository.Get("099153c2625149bc8ecb3e85e03f0022");

            result.IsSuccess.ShouldBeTrue();
            result.Result.RobotId.ShouldBe("099153c2625149bc8ecb3e85e03f0022");
        }

        [Test]
        public void RepositoryUpdateRobotTest()
        {
            //Arrange
            Robot robot = null;
            robot = repository.Get("099153c2625149bc8ecb3e85e03f0022").Result;

            //Action
            robot.MoveHeadForUp();
            var result = repository.Update(robot);

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.HeadAlign.ShouldBe(Align.Top);
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
