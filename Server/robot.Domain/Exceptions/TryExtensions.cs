using System;

namespace robot.Domain.Exceptions
{
    using static Helpers;

    public static class Try
    {
        public static Try<Exception, TSuccess> Run<TSuccess>(this Func<TSuccess> func)
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

        public static Try<Exception, Result> Run(this Action action) => Run(ToFunc(action));
        public static Try<Exception, TSuccess> Run<TSuccess>(this Exception ex) => ex;
    }
}