using NUnit.Framework;
using robot.Domain.Features.Robo;
using Shouldly;

namespace robot.Domain.Test
{
    public class RobotCreatorTests
    {
        AbstractRobotFactory creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteRobotFactory();
        }

        [Test]
        public void ARobotCreatedTest()
        {
            RobotAgreggate robot = creator.MakeARobot();
            //head
                //align
                //direction
            robot.HeadAlign.ShouldBe(Align.Normal);
            robot.HeadDirection.ShouldBe(0);
            //elbow
                //leftelbow
                //rightelbow
            robot.LeftElbowPosition.ShouldBe(180);
            robot.RightElbowPosition.ShouldBe(180);
            //wrist
                //leftwrist
                //rightwrist
            robot.LeftWristDirection.ShouldBe(0);
            robot.RightWristDirection.ShouldBe(0);
        }
    }
    
}