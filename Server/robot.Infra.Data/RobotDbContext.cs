using Microsoft.EntityFrameworkCore;
using robot.Infra.Data.Mappers;

namespace robot.Infra.Data
{
    public class RobotDbContext : DbContext
    {
        public RobotDbContext(DbContextOptions<RobotDbContext> options) : base(options) { }

        public DbSet<RobotDao> Robots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity properties and relationships here
            modelBuilder.Entity<RobotDao>(entity =>
            {
                entity.HasKey(e => e.RobotId);
                entity.Property(e => e.RobotName).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.HeadAlign).IsRequired();
                entity.Property(e => e.HeadDirection).IsRequired();
                entity.Property(e => e.LeftElbowPosition).IsRequired();
                entity.Property(e => e.LeftWristDirection).IsRequired();
                entity.Property(e => e.RightElbowPosition).IsRequired();
                entity.Property(e => e.RightWristDirection).IsRequired();
            });
        }
    }
}
