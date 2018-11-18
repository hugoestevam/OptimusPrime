using robot.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace robot.Domain.Features.Robo
{
    public interface IRobotRepository
    {
        Try<Exception, RobotAgreggate> Add(RobotAgreggate robot);
        Try<Exception, RobotAgreggate> Get(string robotId);
        Try<Exception, List<RobotAgreggate>> GetAll();
        Try<Exception, RobotAgreggate> Update(RobotAgreggate robot);
        Try<Exception, Result> Delete(string robotId);
    }
}
