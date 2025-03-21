using NUnit.Framework;
using robot.Domain.Exceptions;
using robot.Domain.Results;
using robot.Domain.Features.Robo;
using robot.Domain.Test.Initialize;
using Shouldly;

namespace robot.Domain.Test.Entities
{
    public class RobotWristUnitTest
    {
        AbstractRobotFactory creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteRobotFactory();
        }

        [Test]
        public void RotateLeftWristToTheLeftTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed();

            //Action
            var result = robot.LeftWristRotateToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(45);
            robot.LeftWristDirection.ShouldBe(45);
        }

        [Test]
        public void RotateLeftWristToTheRightTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed();

            //Action
            var result = robot.LeftWristRotateToTheRight();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(-45);
            robot.LeftWristDirection.ShouldBe(-45);
        }

        [Test]
        public void RotateRightWristToTheLeftTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithRightElbowCollapsed();

            //Action
            var result = robot.RightWristRotateToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(45);
            robot.RightWristDirection.ShouldBe(45);
        }

        [Test]
        public void RotateRightWristToTheRightTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithRightElbowCollapsed();

            //Action
            var result = robot.RightWristRotateToTheRight();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(-45);
            robot.RightWristDirection.ShouldBe(-45);
        }

        [Test]
        public void RotateLeftWristToTheLeftDenyBecauseItIsLimitedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed()
                                .WithLeftWristLimitedToLeftDirection();

            //Action
            var result = robot.LeftWristRotateToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<LimitedWristRotateException>();
            robot.LeftWristDirection.ShouldBe(180);
        }

        [Test]
        public void RotateLeftWristToTheRightDenyBecauseItIsLimitedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed()
                                .WithLeftWristLimitedToRightDirection();

            //Action
            var result = robot.LeftWristRotateToTheRight();

            //Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<LimitedWristRotateException>();
            robot.LeftWristDirection.ShouldBe(-90);
        }

        [Test]
        public void RotateLeftWristToTheLeftDenyBecauseElbowIsntCollapsedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot());

            //Action
            var result = robot.LeftWristRotateToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<DeniedWristRotateException>();
            robot.LeftWristDirection.ShouldBe(0);
        }

        [Test]
        public void RotateLeftWristToTheRightDenyBecauseElbowIsntCollapsedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot());

            //Action
            var result = robot.LeftWristRotateToTheRight();

            //Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<DeniedWristRotateException>();
            robot.LeftWristDirection.ShouldBe(0);
        }

        [Test]
        public void RotateRightWristToTheRightDenyBecauseElbowIsntCollapsedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot());

            //Action
            var result = robot.RightWristRotateToTheRight();

            //Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<DeniedWristRotateException>();
            robot.RightWristDirection.ShouldBe(0);
        }

        [Test]
        public void RotateRightWristToTheLeftDenyBecauseElbowIsntCollapsedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot());

            //Action
            var result = robot.RightWristRotateToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<DeniedWristRotateException>();
            robot.RightWristDirection.ShouldBe(0);
        }
    }
}
