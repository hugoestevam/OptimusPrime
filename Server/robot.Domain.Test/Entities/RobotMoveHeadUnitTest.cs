using NUnit.Framework;
using robot.Domain.Results;
using robot.Domain.Test.Initialize;
using Shouldly;
using System;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;

namespace robot.Domain.Test
{
    public class RobotMoveHeadUnitTest
    {
        AbstractRobotFactory creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteRobotFactory();
        }

        [Test]
        public void MoveHeadForUpTest()
        {
            //Arrange
            RobotAgreggate robot = creator.MakeARobot();

            //Action
            var result = robot.MoveHeadForUp();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(Align.Top);
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
            result.Value.ShouldBe(Align.Normal);
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<LimitedHeadMoveException>();
            robot.HeadAlign.ShouldBe(Align.Top);
        }

        [Test]
        public void MoveHeadToBelowTest()
        {
            //Arrange
            RobotAgreggate robot = creator.MakeARobot();

            //Action
            var result = robot.MoveHeadToBelow();

            //Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(Align.Botton);
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
            result.Value.ShouldBe(Align.Normal);
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
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBeOfType<LimitedHeadMoveException>();
            robot.HeadAlign.ShouldBe(Align.Botton);
        }
    }
}
