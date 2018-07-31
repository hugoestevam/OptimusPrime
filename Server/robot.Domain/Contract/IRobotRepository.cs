using robot.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace robot.Domain.Contract
{
    public interface IRobotRepository
    {
        Try<Exception, Robot> Add(Robot robot);
        Try<Exception, Robot> Get(string robotId);
        Try<Exception, List<Robot>> GetAll();
        Try<Exception, Robot> Update(Robot robot);
        Try<Exception, Result> Delete(string robotId);
    }
}
