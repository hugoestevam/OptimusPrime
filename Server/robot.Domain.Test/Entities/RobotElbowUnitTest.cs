using NUnit.Framework;
using robot.Domain.Exceptions;
using robot.Domain.Factory;
using robot.Domain.Test.Initialize;
using Shouldly;

namespace robot.Domain.Test.Entities
{
    public class RobotElbowUnitTest
    {
        Creator creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteCreatorRobot();
        }

        [Test]
        public void CollapseLeftElbowTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.LeftElbowCollapse();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(135);
            robot.LeftElbowPosition.ShouldBe(135);
        }

        [Test]
        public void CollapseRightElbowTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.RightElbowCollapse();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(135);
            robot.RightElbowPosition.ShouldBe(135);
        }

        [Test]
        public void ExpandLeftElbowTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed();

            //Action
            var result = robot.LeftElbowExpand();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(90);
            robot.LeftElbowPosition.ShouldBe(90);
        }

        [Test]
        public void ExpandRightElbowTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithRightElbowCollapsed();

            //Action
            var result = robot.RightElbowExpand();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(90);
            robot.RightElbowPosition.ShouldBe(90);
        }

        [Test]
        public void CollapseLeftElbowDenyTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed();

            //Action
            var result = robot.LeftElbowCollapse();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedElbowCollapseException>();
            robot.LeftElbowPosition.ShouldBe(45);
        }

        [Test]
        public void CollapseRightElbowDenyTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithRightElbowCollapsed();

            //Action
            var result = robot.RightElbowCollapse();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedElbowCollapseException>();
            robot.RightElbowPosition.ShouldBe(45);
        }

        [Test]
        public void ExpandLeftElbowDenyTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.LeftElbowExpand();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedElbowExpandException>();
            robot.LeftElbowPosition.ShouldBe(180);
        }

        [Test]
        public void ExpandRightElbowDenyTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.RightElbowExpand();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedElbowExpandException>();
            robot.RightElbowPosition.ShouldBe(180);
        }

        [Test]
        public void CollapseLeftElbowTwoTimesTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            robot.LeftElbowCollapse();
            var result = robot.LeftElbowCollapse();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(90);
            robot.LeftElbowPosition.ShouldBe(90);
        }

        [Test]
        public void ExpandLeftElbowTwoTimesTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftElbowCollapsed();

            //Action
            robot.LeftElbowExpand();
            var result = robot.LeftElbowExpand();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(135);
            robot.LeftElbowPosition.ShouldBe(135);
        }

        [Test]
        public void ExpandLeftElbowAndSetWristToInitialStateTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithLeftWristLimitedToLeftDirection()
                                .WithLeftElbowCollapsed();

            //Action
            var result = robot.LeftElbowExpand();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(90);
            robot.LeftElbowPosition.ShouldBe(90);
            robot.LeftWristDirection.ShouldBe(0);
        }

        [Test]
        public void ExpandRightElbowAndSetWristToInitialStateTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithRightWristLimitedToLeftDirection()
                                .WithRightElbowCollapsed();

            //Action
            var result = robot.RightElbowExpand();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(90);
            robot.RightElbowPosition.ShouldBe(90);
            robot.RightWristDirection.ShouldBe(0);
        }
    }
}
