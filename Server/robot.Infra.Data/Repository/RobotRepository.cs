using robot.Domain.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using robot.Domain.Features.Robo;

namespace robot.Infra.Data
{
    /// <summary>
    /// Está classe é um simples repositório para manter o estado
    /// dos robos em memória
    /// </summary>
    public class RobotRepository : IRobotRepository
    {
        private readonly ConcurrentDictionary<string, RobotAgreggate> _cache;
        public RobotRepository()
        {
            _cache = new ConcurrentDictionary<string, RobotAgreggate>();
            RobotAgreggate robot = new ConcreteRobotFactory().MakeARobot();
            robot.RobotId = "099153c2625149bc8ecb3e85e03f0022";
            robot.RobotName = "EuRobo";
            _cache.GetOrAdd(robot.RobotId, robot);
        }
        public Try<Exception, RobotAgreggate> Add(RobotAgreggate robot)
        {
            var robotId = Guid.NewGuid().ToString("N");

            robot.RobotId = robotId;
            _cache.TryAdd(robotId, robot);
            return robot;
        }

        public Try<Exception, Result> Delete(string robotId)
        {
            RobotAgreggate robot;
            if (_cache.TryRemove(robotId, out robot))
                return Result.Successful;

            return new InvalidOperationException($"Erro ao remover o robo: {robotId}");
        }

        public Try<Exception, RobotAgreggate> Get(string robotId)
        {
            RobotAgreggate robot = null;
            if (_cache.TryGetValue(robotId, out robot))
            {
                return robot;
            }
            return new NotFoundException();
        }

        public Try<Exception, List<RobotAgreggate>> GetAll()
        {
            return Try.Run( () => _cache.Values.ToList());
        }

        public Try<Exception, RobotAgreggate> Update(RobotAgreggate robot)
        {
            RobotAgreggate actualRobot = null;
            _cache.TryGetValue(robot.RobotId, out actualRobot);
            
            if (_cache.TryUpdate(robot.RobotId, robot, actualRobot))
            {
                return robot;
            }

            return new InvalidOperationException($"Erro ao atualizar o robo: {robot.RobotId}");
        }
    }
}
