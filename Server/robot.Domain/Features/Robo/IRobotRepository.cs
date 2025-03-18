using robot.Domain.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace robot.Domain.Features.Robo
{
    public interface IRobotRepository
    {
        Task<Result<Exception, RobotAgreggate>> Add(RobotAgreggate robot);
        Task<Result<Exception, RobotAgreggate>> Get(long robotId);
        Task<Result<Exception, List<RobotAgreggate>>> GetAll();
        Task<Result<Exception, RobotAgreggate>> Update(RobotAgreggate robot);
        Task<Result<Exception, Unit>> Delete(long robotId);
    }
}
