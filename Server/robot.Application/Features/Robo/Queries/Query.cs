using MediatR;
using robot.Domain.Results;
using System;
using System.Collections.Generic;
using robot.Domain.Features.Robo;

namespace robot.Application.Features.Robo.Queries
{
    public class Query : IRequest<Result<Exception, List<RobotAgreggate>>>
    {
    }
}
