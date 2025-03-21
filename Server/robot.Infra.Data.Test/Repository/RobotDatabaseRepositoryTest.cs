using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using robot.Domain;
using robot.Domain.Exceptions;
using robot.Domain.Features.Robo;
using robot.Domain.Results;
using robot.Infra.Data.Mappers;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace robot.Infra.Data.Test
{
    public class RobotDatabaseRepositoryTest
    {
        private IMapper _mapper;
        private DbContextOptions<RobotDbContext> _dbContextOptions;
        private RobotDbContext _context;
        private IRobotRepository _repository;
        private AbstractRobotFactory _robotFactory;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RobotAgreggate, RobotDao>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _dbContextOptions = new DbContextOptionsBuilder<RobotDbContext>()
                .UseInMemoryDatabase(databaseName: "RobotDatabase")
                .Options;

            _context = new RobotDbContext(_dbContextOptions);
            _repository = new RobotDatabaseRepository(_context, _mapper);
            _robotFactory = new ConcreteRobotFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task RepositoryAddRobotTest()
        {
            // Arrange
            var robot = _robotFactory.MakeARobot();

            // Act
            var result = await _repository.Add(robot);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.RobotId.ShouldNotBe(0);
        }

        [Test]
        public async Task RepositoryGetAllRobotTest()
        {
            // Arrange
            var robot = _robotFactory.MountARobot(1, "Test Robot", (int)RobotStatus.Online, (int)Align.Normal, 0, 45, 45, 0, 0);
            var robotDao = _mapper.Map<RobotDao>(robot);

            using (var context = new RobotDbContext(_dbContextOptions))
            {
                context.Robots.Add(robotDao);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _repository.GetAll();

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.Count.ShouldBe(1);
        }

        [Test]
        public async Task RepositoryGetByIdRobotTest()
        {
            //Arrange
            var robot = _robotFactory.MountARobot(1, "Test Robot", (int)RobotStatus.Online, (int)Align.Normal, 0, 45, 45, 0, 0);
            var robotDao = _mapper.Map<RobotDao>(robot);

            using (var context = new RobotDbContext(_dbContextOptions))
            {
                context.Robots.Add(robotDao);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _repository.Get(1);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.RobotId.ShouldBe(1);
        }

        [Test]
        public async Task RepositoryUpdateRobotTest()
        {
            // Arrange
            var robot = _robotFactory.MountARobot(1, "Test Robot", (int)RobotStatus.Online, (int)Align.Normal, 0, 45, 45, 0, 0);
            var robotDao = _mapper.Map<RobotDao>(robot);

            using (var context = new RobotDbContext(_dbContextOptions))
            {
                context.Robots.Add(robotDao);
                await context.SaveChangesAsync();
            }

            robot.RobotName = "Updated Robot";
            robot.Status = RobotStatus.Offline;
            robot.MoveHeadForUp();
            robot.RotateHeadToTheLeft();

            // Act
            var result = await _repository.Update(robot);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.RobotName.ShouldBe("Updated Robot");
            result.Value.Status.ShouldBe(RobotStatus.Offline);
            result.Value.HeadAlign.ShouldBe(Align.Top);
            result.Value.HeadDirection.ShouldBe(45);
        }

        [Test]
        public async Task RepositoryDeleteRobotTest()
        {
            // Arrange
            var robot = _robotFactory.MountARobot(1, "Test Robot", (int)RobotStatus.Online, (int)Align.Normal, 0, 45, 45, 0, 0);
            var robotDao = _mapper.Map<RobotDao>(robot);

            using (var context = new RobotDbContext(_dbContextOptions))
            {
                context.Robots.Add(robotDao);
                await context.SaveChangesAsync();
            }

            // Act
            var result = await _repository.Delete(1);
            var resultGet = await _repository.Get(1);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            resultGet.IsSuccess.ShouldBeFalse();
            resultGet.Error.ShouldBeOfType<NotFoundException>();
        }
    }
}
