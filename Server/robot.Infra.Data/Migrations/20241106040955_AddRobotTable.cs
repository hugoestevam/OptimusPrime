using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace robot.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRobotTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Robots",
                columns: table => new
                {
                    RobotId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RobotName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    HeadAlign = table.Column<int>(type: "integer", nullable: false),
                    HeadDirection = table.Column<int>(type: "integer", nullable: false),
                    LeftElbowPosition = table.Column<int>(type: "integer", nullable: false),
                    RightElbowPosition = table.Column<int>(type: "integer", nullable: false),
                    LeftWristDirection = table.Column<int>(type: "integer", nullable: false),
                    RightWristDirection = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robots", x => x.RobotId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Robots");
        }
    }
}
