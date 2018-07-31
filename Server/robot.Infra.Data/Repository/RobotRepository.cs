using robot.Domain;
using robot.Domain.Contract;
using robot.Domain.Exceptions;
using robot.Domain.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace robot.Infra.Data
{
    /// <summary>
    /// Está classe é um simples repositório para manter o estado
    /// dos robos em memória
    /// </summary>
    public class RobotRepository : IRobotRepository
    {
        private readonly ConcurrentDictionary<string, Robot> _cache;
        public RobotRepository()
        {
            _cache = new ConcurrentDictionary<string, Robot>();
            Robot robot = new ConcreteCreatorRobot().MakeARobot();
            robot.RobotId = "099153c2625149bc8ecb3e85e03f0022";
            robot.RobotName = "EuRobo";
            _cache.GetOrAdd(robot.RobotId, robot);
        }
        public Try<Exception, Robot> Add(Robot robot)
        {
            var robotId = Guid.NewGuid().ToString("N");

            robot.RobotId = robotId;
            _cache.TryAdd(robotId, robot);
            return robot;
        }

        public Try<Exception, Result> Delete(string robotId)
        {
            Robot robot;
            if (_cache.TryRemove(robotId, out robot))
                return Result.Successful;

            return new InvalidOperationException($"Erro ao remover o robo: {robotId}");
        }

        public Try<Exception, Robot> Get(string robotId)
        {
            Robot robot = null;
            if (_cache.TryGetValue(robotId, out robot))
            {
                return robot;
            }
            return new NotFoundException();
        }

        public Try<Exception, List<Robot>> GetAll()
        {
            return Try.Run( () => _cache.Values.ToList());
        }

        public Try<Exception, Robot> Update(Robot robot)
        {
            Robot actualRobot = null;
            _cache.TryGetValue(robot.RobotId, out actualRobot);
            
            if (_cache.TryUpdate(robot.RobotId, robot, actualRobot))
            {
                return robot;
            }

            return new InvalidOperationException($"Erro ao atualizar o robo: {robot.RobotId}");
        }
    }
}
