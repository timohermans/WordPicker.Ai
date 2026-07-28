using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WordPicker.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPickHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPickCycle",
                table: "WordLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastPickCycle",
                table: "WordEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PickHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WordEntryId = table.Column<int>(type: "integer", nullable: false),
                    WordListId = table.Column<int>(type: "integer", nullable: false),
                    PickedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickHistories_WordEntries_WordEntryId",
                        column: x => x.WordEntryId,
                        principalTable: "WordEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickHistories_WordLists_WordListId",
                        column: x => x.WordListId,
                        principalTable: "WordLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickHistories_WordEntryId",
                table: "PickHistories",
                column: "WordEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_PickHistories_WordListId",
                table: "PickHistories",
                column: "WordListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickHistories");

            migrationBuilder.DropColumn(
                name: "CurrentPickCycle",
                table: "WordLists");

            migrationBuilder.DropColumn(
                name: "LastPickCycle",
                table: "WordEntries");
        }
    }
}
