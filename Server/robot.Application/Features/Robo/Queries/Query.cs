using MediatR;
using robot.Domain.Exceptions;
using System;
using System.Collections.Generic;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Queries
{
    public class Query : IRequest<Try<Exception, List<RobotAgreggate>>>
    {
    }
}
