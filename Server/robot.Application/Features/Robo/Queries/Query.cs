using MediatR;
using robot.Domain;
using robot.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace robot.Application.Features.Robo.Queries
{
    public class Query : IRequest<Try<Exception, List<Robot>>>
    {
    }
}
