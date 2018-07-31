using NUnit.Framework;
using robot.Domain.Exceptions;
using robot.Domain.Factory;
using robot.Domain.Test.Initialize;
using Shouldly;
using System;

namespace robot.Domain.Test
{
    public class RobotRotateHeadUnitTest
    {
        Creator creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteCreatorRobot();
        }

        [Test]
        public void RotateHeadToTheLeftTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.RotateHeadToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(45);
            robot.HeadDirection.ShouldBe(45);
        }

        [Test]
        public void RotateHeadToTheRightTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.RotateHeadToTheRight();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(-45);
            robot.HeadDirection.ShouldBe(-45);
        }

        [Test]
        public void RotateHeadToTheRightDenyBecauseAlignIsBottonTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithHeadToBelow();

            //Action
            var result = robot.RotateHeadToTheRight();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<DeniedHeadRotateException>();
            robot.HeadDirection.ShouldBe(0);
        }

        [Test]
        public void RotateHeadToTheLeftDenyBecauseAlignIsBottonTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithHeadToBelow();

            //Action
            var result = robot.RotateHeadToTheLeft();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<DeniedHeadRotateException>();
            robot.HeadDirection.ShouldBe(0);
        }

        [Test]
        public void RotateHeadToTheLeftDenyBecauseDirectionLeftIsLimitedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithHeadDirectionToLeftIsLimited();

            //Action
            var result = robot.RotateHeadToTheLeft();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedHeadRotateException>();
            robot.HeadDirection.ShouldBe(90);
        }

        [Test]
        public void RotateHeadToTheRightDenyBecauseDirectionRightIsLimitedTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator
                                .MakeARobot())
                                .WithHeadDirectionToRightIsLimited();

            //Action
            var result = robot.RotateHeadToTheRight();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedHeadRotateException>();
            robot.HeadDirection.ShouldBe(-90);
        }

        [Test]
        public void RotateHeadToTheLeftTwoTimesTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            robot.RotateHeadToTheLeft();
            var result = robot.RotateHeadToTheLeft();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(90);
            robot.HeadDirection.ShouldBe(90);
        }

        [Test]
        public void RotateHeadToTheRightTwoTimesTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            robot.RotateHeadToTheRight();
            var result = robot.RotateHeadToTheRight();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(-90);
            robot.HeadDirection.ShouldBe(-90);
        }
    }
}
