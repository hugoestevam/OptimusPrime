using robot.Domain.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace robot.Domain.Features.Robo
{
    public interface IRobotRepository
    {
        Task<Result<RobotAgreggate>> Add(RobotAgreggate robot);
        Task<Result<RobotAgreggate>> Get(long robotId);
        Task<Result<List<RobotAgreggate>>> GetAll();
        Task<Result<RobotAgreggate>> Update(RobotAgreggate robot);
        Task<Result<Unit>> Delete(long robotId);
    }
}
