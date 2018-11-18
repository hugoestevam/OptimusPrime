using robot.Domain.Results;
using System;
using System.Collections.Generic;

namespace robot.Domain.Features.Robo
{
    public interface IRobotRepository
    {
        Result<Exception, RobotAgreggate> Add(RobotAgreggate robot);
        Result<Exception, RobotAgreggate> Get(string robotId);
        Result<Exception, List<RobotAgreggate>> GetAll();
        Result<Exception, RobotAgreggate> Update(RobotAgreggate robot);
        Result<Exception, Unit> Delete(string robotId);
    }
}
