using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Domain.Exceptions
{
    public class DeniedHeadRotateException : BussinessException
    {
        public DeniedHeadRotateException() : base(ErrorCodes.PreconditionFailed,
            "Não é possível rotacionar a cabeça quando ela está alinhada para baixo.")
        {
        }
    }    
}
