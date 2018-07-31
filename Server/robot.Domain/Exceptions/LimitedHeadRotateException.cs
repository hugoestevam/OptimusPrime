namespace robot.Domain.Exceptions
{
    public class LimitedHeadRotateException : BussinessException
    {
        public LimitedHeadRotateException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível rotacionar a cabeça, pois ela já está no seu limite.")
        {
        }
    }
}