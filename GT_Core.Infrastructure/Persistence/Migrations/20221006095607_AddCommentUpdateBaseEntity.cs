using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GT_Core.Infrastructure.Persistence.Migrations
{
    public partial class AddCommentUpdateBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_Tester",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Tester",
                table: "Tickets",
                newName: "Consultant");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_Tester",
                table: "Tickets",
                newName: "IX_Tickets_Consultant");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TicketId",
                table: "Comments",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_Consultant",
                table: "Tickets",
                column: "Consultant",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_Consultant",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.RenameColumn(
                name: "Consultant",
                table: "Tickets",
                newName: "Tester");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_Consultant",
                table: "Tickets",
                newName: "IX_Tickets_Tester");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_Tester",
                table: "Tickets",
                column: "Tester",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}