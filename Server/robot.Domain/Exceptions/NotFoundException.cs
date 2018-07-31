namespace robot.Domain.Exceptions
{
    public class NotFoundException : BussinessException
    {
        public NotFoundException() : base(ErrorCodes.NotFound,
            "Robo não encontrado!")
        {
        }
    }
}