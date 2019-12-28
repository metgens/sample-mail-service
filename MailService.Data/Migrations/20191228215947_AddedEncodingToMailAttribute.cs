using Microsoft.EntityFrameworkCore.Migrations;

namespace MailService.Data.Migrations
{
    public partial class AddedEncodingToMailAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentType",
                schema: "ms",
                table: "MailAttachments",
                newName: "MediaType");

            migrationBuilder.AddColumn<string>(
                name: "Encoding",
                schema: "ms",
                table: "MailAttachments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Encoding",
                schema: "ms",
                table: "MailAttachments");

            migrationBuilder.RenameColumn(
                name: "MediaType",
                schema: "ms",
                table: "MailAttachments",
                newName: "ContentType");
        }
    }
}
