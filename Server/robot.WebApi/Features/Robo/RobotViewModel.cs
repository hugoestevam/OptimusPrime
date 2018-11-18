namespace robot.WebApi.Features.Robo
{
    public class RobotViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int HeadAlign { get; set; }
        public int HeadDirection { get; set; }
        public int LeftElbowPosition { get; set; }
        public int RightElbowPosition { get; set; }
        public int LeftWristDirection { get; set; }
        public int RightWristDirection { get; set; }
    }
}