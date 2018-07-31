namespace robot.Domain.Exceptions
{
    public class DeniedWristRotateException : BussinessException
    {
        public DeniedWristRotateException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível rotacionar o pulso quando o cotovelo não está fortemente contraído.")
        {
        }
    }
}