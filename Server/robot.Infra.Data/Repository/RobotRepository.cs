using robot.Domain.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;
using robot.Domain;
using OpenTelemetry.Trace;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace robot.Infra.Data
{
    /// <summary>
    /// Esta classe é um simples repositório para manter o estado
    /// dos robôs em memória
    /// </summary>
    public class RobotRepository : IRobotRepository
    {
        private readonly ConcurrentDictionary<long, RobotAgreggate> _robots;
        private readonly Tracer _tracer;

        public RobotRepository()
        {
            _robots = new ConcurrentDictionary<long, RobotAgreggate>();
            _tracer = DataTelemetry.Instance.GetTracer("RobotInfraData");

            RobotAgreggate robot = new ConcreteRobotFactory().MakeARobot();
            robot.RobotId = 0991532625149;
            robot.RobotName = "EuRobo";
            robot.Status = RobotStatus.Online;
            _robots[robot.RobotId] = robot;
        }

        public async Task<Result<RobotAgreggate>> Add(RobotAgreggate robot)
        {
            return await Task.FromResult(Result<RobotAgreggate>.TryRun(() =>
            {
                var robotId = (long)new Random().Next(int.MaxValue);
                robot.RobotId = robotId;
                _robots[robotId] = robot;
                return Result<RobotAgreggate>.Success(robot);
            }));
        }

        public async Task<Result<Unit>> Delete(long robotId)
        {
            return await Task.FromResult(Result<Unit>.TryRun(() =>
            {
                if (_robots.TryRemove(robotId, out _))
                {
                    return Result<Unit>.Success(Unit.Successful);
                }
                return Result<Unit>.Fail(new InvalidOperationException($"Erro ao remover o robo: {robotId}"));
            }));
        }

        public async Task<Result<RobotAgreggate>> Get(long robotId)
        {
            return await Task.FromResult(Result<RobotAgreggate>.TryRun(() =>
            {
                if (_robots.TryGetValue(robotId, out RobotAgreggate robot))
                {
                    return Result<RobotAgreggate>.Success(robot);
                }
                return Result<RobotAgreggate>.Fail(new NotFoundException());
            }));
        }

        public async Task<Result<List<RobotAgreggate>>> GetAll()
        {
            using (var span = _tracer.StartActiveSpan("GetAllRobots"))
            {
                return await Task.FromResult(Result<List<RobotAgreggate>>.TryRun(() =>
                {
                    var result = _robots.Values.ToList();
                    span.SetAttribute("robot.count", result.Count);
                    return Result<List<RobotAgreggate>>.Success(result);
                }));
            }
        }

        public async Task<Result<RobotAgreggate>> Update(RobotAgreggate robot)
        {
            return await Task.FromResult(Result<RobotAgreggate>.TryRun(() =>
            {
                if (_robots.ContainsKey(robot.RobotId))
                {
                    _robots[robot.RobotId] = robot;
                    return Result<RobotAgreggate>.Success(robot);
                }
                return Result<RobotAgreggate>.Fail(new NotFoundException());
            }));
        }
    }
}
