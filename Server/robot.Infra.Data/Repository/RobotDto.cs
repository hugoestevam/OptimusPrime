using System;
using robot.Domain.Features.Robo;

namespace robot.Infra.Data.Mappers
{
    public class RobotDao
    {
        public long RobotId { get; set; }
        public string RobotName { get; set; }
        public int Status { get; set; }
        public int HeadAlign { get; set; }
        public int HeadDirection { get; set; }
        public int LeftElbowPosition { get; set; }
        public int RightElbowPosition { get; set; }
        public int LeftWristDirection { get; set; }
        public int RightWristDirection { get; set; }
    }
}
