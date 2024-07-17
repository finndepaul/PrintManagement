using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintManagement.Infrastructure.Migrations
{
    public partial class _4st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Team_TeamId",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 17, 15, 33, 42, 656, DateTimeKind.Local).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 17, 15, 33, 42, 656, DateTimeKind.Local).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 17, 15, 33, 42, 656, DateTimeKind.Local).AddTicks(8685));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                columns: new[] { "CreateTime", "DateOfBirth", "Email", "Password" },
                values: new object[] { new DateTime(2024, 7, 17, 15, 33, 42, 788, DateTimeKind.Local).AddTicks(4502), new DateTime(2024, 7, 17, 15, 33, 42, 788, DateTimeKind.Local).AddTicks(4515), "mai@gmail.com", "$2a$11$U/wMks1fenp7myp5dZYT4O/41P/dYBA9jmMPlvCvdmJBCQGeSC5Sa" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 17, 15, 33, 42, 656, DateTimeKind.Local).AddTicks(8698), new DateTime(2024, 7, 17, 15, 33, 42, 656, DateTimeKind.Local).AddTicks(8699), "$2a$11$CaPeRav/trIaJfZVDn6kSOTzGlqMSsC7m.zps4.PFyGLm/8sUrxAG" });

            migrationBuilder.AddForeignKey(
                name: "FK_User_Team_TeamId",
                table: "User",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Team_TeamId",
                table: "User");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 12, 8, 11, 26, 698, DateTimeKind.Local).AddTicks(9490));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 12, 8, 11, 26, 698, DateTimeKind.Local).AddTicks(9476));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 12, 8, 11, 26, 698, DateTimeKind.Local).AddTicks(9492));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                columns: new[] { "CreateTime", "DateOfBirth", "Email", "Password" },
                values: new object[] { new DateTime(2024, 7, 12, 8, 11, 26, 828, DateTimeKind.Local).AddTicks(1295), new DateTime(2024, 7, 12, 8, 11, 26, 828, DateTimeKind.Local).AddTicks(1311), "abcd@gmail.com", "$2a$11$.DbmojaQCaAl7Zj87oDPMekJT8MiNo965BmkYXbIzy7S/fEl4aE/e" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 12, 8, 11, 26, 698, DateTimeKind.Local).AddTicks(9503), new DateTime(2024, 7, 12, 8, 11, 26, 698, DateTimeKind.Local).AddTicks(9504), "$2a$11$6QZI4dWlptyV47fZtFr/4elDPcDKvRhg5cm4OBKHbGFX.DOAsDKT." });

            migrationBuilder.AddForeignKey(
                name: "FK_User_Team_TeamId",
                table: "User",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
