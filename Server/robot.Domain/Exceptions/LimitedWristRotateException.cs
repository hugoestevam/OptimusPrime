namespace robot.Domain.Exceptions
{
    public class LimitedWristRotateException : BussinessException
    {
        public LimitedWristRotateException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível rotacionar o pulso, pois ele já está no seu limite.")
        {
        }
    }
}