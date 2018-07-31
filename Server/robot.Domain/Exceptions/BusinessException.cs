using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Domain.Exceptions
{
    public class BussinessException : Exception
    {
        public BussinessException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ErrorCodes ErrorCode { get; }
    }
}
