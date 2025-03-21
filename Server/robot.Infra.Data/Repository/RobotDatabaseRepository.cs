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

        public async Task<Result<RobotAgreggate>> Add(RobotAgreggate robot)
        {
            return await Result<RobotAgreggate>.TryRunAsync(async () =>
            {
                var robotDao = _mapper.Map<RobotDao>(robot);
                var newRobot = await _context.Robots.AddAsync(robotDao);
                await _context.SaveChangesAsync();
                robot.RobotId = newRobot.Entity.RobotId;
                return robot;
            });
        }

        public async Task<Result<Unit>> Delete(long robotId)
        {
            return await Result<Unit>.TryRunAsync(async () =>
            {
                var robot = await _context.Robots.FindAsync(robotId);
                if (robot == null)
                {
                    return Result<Unit>.Fail(new NotFoundException());
                }

                _context.Robots.Remove(robot);
                await _context.SaveChangesAsync();
                return Unit.Successful;
            });
        }

        public async Task<Result<RobotAgreggate>> Get(long robotId)
        {
            return await Result<RobotAgreggate>.TryRunAsync(async () =>
            {
                var robot = await _context.Robots.FindAsync(robotId);
                if (robot == null)
                    return Result<RobotAgreggate>.Fail(new NotFoundException());

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
            });
        }

        public async Task<Result<List<RobotAgreggate>>> GetAll()
        {
            using (var span = _tracer.StartActiveSpan("GetAllRobots"))
            {
                return await Result<List<RobotAgreggate>>.TryRunAsync(async () =>
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
                });
            }
        }

        public async Task<Result<RobotAgreggate>> Update(RobotAgreggate robot)
        {
            return await Result<RobotAgreggate>.TryRunAsync(async () =>
            {
                var existingRobot = await _context.Robots.FindAsync(robot.RobotId);
                if (existingRobot == null)
                {
                    return Result<RobotAgreggate>.Fail( new NotFoundException());
                }

                _context.Entry(existingRobot).CurrentValues.SetValues(robot);
                await _context.SaveChangesAsync();
                return robot;
            });
        }
    }
}
