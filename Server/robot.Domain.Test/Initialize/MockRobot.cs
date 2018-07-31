using System;

namespace robot.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class MockRobot : Robot
    {        
        public MockRobot()
        {
            Head = new Head(Align.Normal, 0);
            LeftElbow = new Elbow(180);
            RightElbow = new Elbow(180);
            LeftWrist = new Wrist(0);
            RightWrist = new Wrist(0);
        }

        public MockRobot WithHeadForUp()
        {
            Head = new Head(Align.Top, 0);
            return this;
        }

        public MockRobot WithHeadToBelow()
        {
            Head = new Head(Align.Botton, 0);
            return this;
        }

        public MockRobot WithHeadDirectionToLeftIsLimited()
        {
            Head = new Head(Align.Normal, 90);
            return this;
        }

        public MockRobot WithHeadDirectionToRightIsLimited()
        {
            Head = new Head(Align.Normal, -90);
            return this;
        }

        public MockRobot WithLeftElbowCollapsed()
        {
            LeftElbow = new Elbow(45);
            return this;
        }

        public MockRobot WithRightElbowCollapsed()
        {
            RightElbow = new Elbow(45);
            return this;
        }

        public MockRobot WithLeftWristLimitedToLeftDirection()
        {
            LeftWrist = new Wrist(180);
            return this;
        }

        public MockRobot WithLeftWristLimitedToRightDirection()
        {
            LeftWrist = new Wrist(-90);
            return this;
        }
    }
}
