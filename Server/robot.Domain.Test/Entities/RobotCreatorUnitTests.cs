using NUnit.Framework;
using robot.Domain;
using robot.Domain.Factory;
using Shouldly;

namespace Tests
{
    public class RobotCreatorUnitTests
    {
        Creator creator;

        [SetUp]
        public void Setup()
        {
            creator = new ConcreteCreatorRobot();
        }

        [Test]
        public void ARobotCreatedTest()
        {
            Robot robot = creator.MakeARobot();
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