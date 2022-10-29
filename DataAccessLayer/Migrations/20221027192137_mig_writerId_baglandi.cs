using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migwriterIdbaglandi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterID",
                table: "Comments",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WriterID",
                table: "Comments",
                column: "WriterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Writers_WriterID",
                table: "Comments",
                column: "WriterID",
                principalTable: "Writers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Writers_WriterID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_WriterID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WriterID",
                table: "Comments");
        }
    }
}
