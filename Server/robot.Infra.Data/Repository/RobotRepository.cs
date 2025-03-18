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

namespace robot.Infra.Data
{
    /// <summary>
    /// Está classe é um simples repositório para manter o estado
    /// dos robos em memória
    /// </summary>
    public class RobotRepository : IRobotRepository
    {
        private readonly ConcurrentDictionary<long, RobotAgreggate> _cache;
        private readonly Tracer _tracer;

        public RobotRepository()
        {
            _cache = new ConcurrentDictionary<long, RobotAgreggate>();
            _tracer = DataTelemetry.Instance.GetTracer("RobotInfraData");

            RobotAgreggate robot = new ConcreteRobotFactory().MakeARobot();
            robot.RobotId = 0991532625149;
            robot.RobotName = "EuRobo";
            robot.Status = RobotStatus.Online;
            _cache.GetOrAdd(robot.RobotId, robot);
        }

        public async Task<Result<Exception, RobotAgreggate>> Add(RobotAgreggate robot)
        {
            var robotId = (long)new Random(1).Next(int.MaxValue);

            robot.RobotId = robotId;
            _cache.TryAdd(robotId, robot);
            return await Task.FromResult(robot);
        }

        public async Task<Result<Exception, Unit>> Delete(long robotId)
        {
            RobotAgreggate robot;
            if (_cache.TryRemove(robotId, out robot))
                return await Task.FromResult(Unit.Successful);

            return await Task.FromResult(new InvalidOperationException($"Erro ao remover o robo: {robotId}"));
        }

        public async Task<Result<Exception, RobotAgreggate>> Get(long robotId)
        {
            RobotAgreggate robot = null;
            if (_cache.TryGetValue(robotId, out robot))
            {
                return await Task.FromResult(robot);
            }
            return await Task.FromResult(new NotFoundException());
        }

        public async Task<Result<Exception, List<RobotAgreggate>>> GetAll()
        {
            using (var span = _tracer.StartActiveSpan("GetAllRobots"))
            {
                try
                {
                    var result = Result.Run(() => _cache.Values.ToList());
                    span.SetAttribute("robot.count", result.Success?.Count ?? 0);
                    return await Task.FromResult(result);
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
            RobotAgreggate actualRobot = null;
            _cache.TryGetValue(robot.RobotId, out actualRobot);

            if (_cache.TryUpdate(robot.RobotId, robot, actualRobot))
            {
                return await Task.FromResult(robot);
            }

            return await Task.FromResult(new InvalidOperationException($"Erro ao atualizar o robo: {robot.RobotId}"));
        }
    }
}
