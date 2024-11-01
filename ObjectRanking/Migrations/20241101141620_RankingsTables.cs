using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObjectRanking.Migrations
{
    /// <inheritdoc />
    public partial class RankingsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RankingSurveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    RelationCap = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RankingSurveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSurveyRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveyRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TourNumber = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserRankTableUrl = table.Column<string>(type: "text", nullable: true),
                    DistanceMatrixUrl = table.Column<string>(type: "text", nullable: true),
                    DistanceMedian = table.Column<int>(type: "integer", nullable: true),
                    DistanceSum = table.Column<int>(type: "integer", nullable: true),
                    RelationMatrixUrl = table.Column<string>(type: "text", nullable: true),
                    RelationGraphUrl = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_RankingSurveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "RankingSurveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSurveys",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSurveys", x => new { x.UserId, x.SurveyId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserSurveys_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSurveys_RankingSurveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "RankingSurveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSurveys_UserSurveyRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserSurveyRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRankings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    TourId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    RankMatrixUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRankings", x => new { x.UserId, x.TourId, x.SurveyId });
                    table.ForeignKey(
                        name: "FK_UserRankings_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRankings_RankingSurveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "RankingSurveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRankings_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRankingObject",
                columns: table => new
                {
                    UserRankingId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRankingUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRankingTourId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRankingSurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRankingObject", x => new { x.UserRankingId, x.ObjectId });
                    table.ForeignKey(
                        name: "FK_UserRankingObject_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRankingObject_UserRankings_UserRankingUserId_UserRankin~",
                        columns: x => new { x.UserRankingUserId, x.UserRankingTourId, x.UserRankingSurveyId },
                        principalTable: "UserRankings",
                        principalColumns: new[] { "UserId", "TourId", "SurveyId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_SurveyId",
                table: "Tours",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRankingObject_ObjectId",
                table: "UserRankingObject",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRankingObject_UserRankingUserId_UserRankingTourId_UserR~",
                table: "UserRankingObject",
                columns: new[] { "UserRankingUserId", "UserRankingTourId", "UserRankingSurveyId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRankings_SurveyId",
                table: "UserRankings",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRankings_TourId",
                table: "UserRankings",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveys_RoleId",
                table: "UserSurveys",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveys_SurveyId",
                table: "UserSurveys",
                column: "SurveyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "UserRankingObject");

            migrationBuilder.DropTable(
                name: "UserSurveys");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "UserRankings");

            migrationBuilder.DropTable(
                name: "UserSurveyRoles");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "RankingSurveys");
        }
    }
}
