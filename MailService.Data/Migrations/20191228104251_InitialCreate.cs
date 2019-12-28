using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailService.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ms");

            migrationBuilder.CreateTable(
                name: "Mails",
                schema: "ms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    IsHtml = table.Column<bool>(nullable: false),
                    Priority = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    SentDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailAttachments",
                schema: "ms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MailId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailAttachments_Mails_MailId",
                        column: x => x.MailId,
                        principalSchema: "ms",
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailAttachments_MailId",
                schema: "ms",
                table: "MailAttachments",
                column: "MailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailAttachments",
                schema: "ms");

            migrationBuilder.DropTable(
                name: "Mails",
                schema: "ms");
        }
    }
}
