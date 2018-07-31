namespace robot.Domain.Exceptions
{
    public class LimitedElbowCollapseException : BussinessException
    {
        public LimitedElbowCollapseException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível contrair o colotovelo, pois ele já está no seu limite.")
        {
        }
    }
}