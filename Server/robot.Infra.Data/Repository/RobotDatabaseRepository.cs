using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;
using robot.Domain.Results;
using robot.Infra.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace robot.Infra.Data
{
    public class RobotDatabaseRepository : IRobotRepository
    {
        private readonly RobotDbContext _context;
        private readonly IMapper _mapper;
        private readonly Tracer _tracer;

        public RobotDatabaseRepository(RobotDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _tracer = DataTelemetry.Instance.GetTracer("RobotInfraData");
        }

        public async Task<Result<Exception, RobotAgreggate>> Add(RobotAgreggate robot)
        {
            try
            {
                var robotDao = _mapper.Map<RobotDao>(robot);
                var newRobot = await _context.Robots.AddAsync(robotDao);
                await _context.SaveChangesAsync();
                robot.RobotId = newRobot.Entity.RobotId;
                return robot;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Result<Exception, Unit>> Delete(long robotId)
        {
            try
            {
                var robot = await _context.Robots.FindAsync(robotId);
                if (robot == null)
                {
                    return Result<Exception, Unit>.Of(new NotFoundException());
                }

                _context.Robots.Remove(robot);
                await _context.SaveChangesAsync();
                return Unit.Successful;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Result<Exception, RobotAgreggate>> Get(long robotId)
        {
            try
            {
                var robot = await _context.Robots.FindAsync(robotId);
                if (robot == null)
                {
                    return Result<Exception, RobotAgreggate>.Of(new NotFoundException());
                }

                var factory = new ConcreteRobotFactory();
                return factory.MountARobot(
                    robot.RobotId,
                    robot.RobotName,
                    robot.Status,
                    robot.HeadAlign,
                    robot.HeadDirection,
                    robot.LeftElbowPosition,
                    robot.LeftWristDirection,
                    robot.RightElbowPosition,
                    robot.RightWristDirection
                );
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public async Task<Result<Exception, List<RobotAgreggate>>> GetAll()
        {
            using (var span = _tracer.StartActiveSpan("GetAllRobots"))
            {
                try
                {
                    var robots = await _context.Robots.ToListAsync();
                    var factory = new ConcreteRobotFactory();
                    var robotAggregates = robots.Select(robot => factory.MountARobot(
                        robot.RobotId,
                        robot.RobotName,
                        robot.Status,
                        robot.HeadAlign,
                        robot.HeadDirection,
                        robot.LeftElbowPosition,
                        robot.LeftWristDirection,
                        robot.RightElbowPosition,
                        robot.RightWristDirection
                    )).ToList();
                    span.SetAttribute("robot.count", robotAggregates.Count);
                    return robotAggregates;
                }
                catch (Exception ex)
                {
                    span.SetStatus(Status.Error.WithDescription(ex.Message));
                    return ex;
                }
            }
        }

        public async Task<Result<Exception, RobotAgreggate>> Update(RobotAgreggate robot)
        {
            try
            {
                var existingRobot = await _context.Robots.FindAsync(robot.RobotId);
                if (existingRobot == null)
                {
                    return Result<Exception, RobotAgreggate>.Of(new NotFoundException());
                }

                _context.Entry(existingRobot).CurrentValues.SetValues(robot);
                await _context.SaveChangesAsync();
                return robot;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
