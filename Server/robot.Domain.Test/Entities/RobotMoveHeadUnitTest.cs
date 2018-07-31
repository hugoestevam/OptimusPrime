using NUnit.Framework;
using robot.Domain.Exceptions;
using robot.Domain.Factory;
using robot.Domain.Test.Initialize;
using Shouldly;
using System;

namespace robot.Domain.Test
{
    public class RobotMoveHeadUnitTest
    {
        Creator creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteCreatorRobot();
        }

        [Test]
        public void MoveHeadForUpTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.MoveHeadForUp();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(Align.Top);
            robot.HeadAlign.ShouldBe(Align.Top);
        }

        [Test]
        public void MoveHeadForUpWhenAlignIsBottonTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot())
                                .WithHeadToBelow();

            //Action
            var result = robot.MoveHeadForUp();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(Align.Normal);
            robot.HeadAlign.ShouldBe(Align.Normal);
        }

        [Test]
        public void MoveHeadForUpDenyTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot())
                                .WithHeadForUp();                                  

            //Action
            var result = robot.MoveHeadForUp();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedHeadMoveException>();
            robot.HeadAlign.ShouldBe(Align.Top);
        }

        [Test]
        public void MoveHeadToBelowTest()
        {
            //Arrange
            Robot robot = creator.MakeARobot();

            //Action
            var result = robot.MoveHeadToBelow();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(Align.Botton);
            robot.HeadAlign.ShouldBe(Align.Botton);
        }

        [Test]
        public void MoveHeadToBelowWhenAlignIsTopTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot())
                                .WithHeadForUp();

            //Action
            var result = robot.MoveHeadToBelow();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Result.ShouldBe(Align.Normal);
            robot.HeadAlign.ShouldBe(Align.Normal);
        }

        [Test]
        public void MoveHeadToBelowDenyTest()
        {
            //Arrange
            creator = new MockCreatorRobot();
            MockRobot robot = ((MockRobot)creator.MakeARobot())
                                .WithHeadToBelow();

            //Action
            var result = robot.MoveHeadToBelow();

            //Assert
            result.IsFailure.ShouldBeTrue();
            result.Failure.ShouldBeOfType<LimitedHeadMoveException>();
            robot.HeadAlign.ShouldBe(Align.Botton);
        }
    }
}
