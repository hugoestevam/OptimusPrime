namespace robot.Domain.Exceptions
{
    public class LimitedHeadMoveException : BussinessException
    {
        public LimitedHeadMoveException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível mover verticalmente a cabeça, pois ela já está no seu limite.")
        {
        }
    }    
}