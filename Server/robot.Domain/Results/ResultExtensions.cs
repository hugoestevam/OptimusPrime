using System;

namespace robot.Domain.Results
{
    using static Helpers;

    public static class Result
    {
        public static Result<Exception, TSuccess> Run<TSuccess>(this Func<TSuccess> func)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public static Result<Exception, Unit> Run(this Action action) => Run(ToFunc(action));
        public static Result<Exception, TSuccess> Run<TSuccess>(this Exception ex) => ex;
    }
}