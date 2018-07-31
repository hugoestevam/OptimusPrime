namespace robot.Domain.Exceptions
{
    public class LimitedElbowExpandException : BussinessException
    {
        public LimitedElbowExpandException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível estender o colotovelo, pois ele já está no seu limite.")
        {
        }
    }
}