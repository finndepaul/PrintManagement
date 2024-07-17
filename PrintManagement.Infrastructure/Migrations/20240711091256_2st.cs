using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintManagement.Infrastructure.Migrations
{
    public partial class _2st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 11, 16, 12, 55, 679, DateTimeKind.Local).AddTicks(4084));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 11, 16, 12, 55, 679, DateTimeKind.Local).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 11, 16, 12, 55, 679, DateTimeKind.Local).AddTicks(4088));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 11, 16, 12, 55, 815, DateTimeKind.Local).AddTicks(7545), new DateTime(2024, 7, 11, 16, 12, 55, 815, DateTimeKind.Local).AddTicks(7567), "$2a$11$ag7.iootFV5K10quuXAlEOHnUMmrySIWX.Pvps05GW4b9oLFNuu7q" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password", "UserName" },
                values: new object[] { new DateTime(2024, 7, 11, 16, 12, 55, 679, DateTimeKind.Local).AddTicks(4170), new DateTime(2024, 7, 11, 16, 12, 55, 679, DateTimeKind.Local).AddTicks(4171), "$2a$11$vlJk7bfJ7Aas5P8PjJrYuO8N1LZE2kqM2XY4gcMotXX380/TehmEi", "dong" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 11, 16, 11, 35, 282, DateTimeKind.Local).AddTicks(9849));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 11, 16, 11, 35, 282, DateTimeKind.Local).AddTicks(9830));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 11, 16, 11, 35, 282, DateTimeKind.Local).AddTicks(9852));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 11, 16, 11, 35, 420, DateTimeKind.Local).AddTicks(7973), new DateTime(2024, 7, 11, 16, 11, 35, 420, DateTimeKind.Local).AddTicks(7989), "$2a$11$Pa4Q6vKGTfLvQf2rc6GDselaVX0sGEpUn5nnZO4/rCL88JOJGH2Pi" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password", "UserName" },
                values: new object[] { new DateTime(2024, 7, 11, 16, 11, 35, 282, DateTimeKind.Local).AddTicks(9867), new DateTime(2024, 7, 11, 16, 11, 35, 282, DateTimeKind.Local).AddTicks(9867), "$2a$11$hSNQTNyrCWNXOrBJBz67R.sA5JOUQFiiHGXyNCWwy0iKsLzPc2Nay", "abc" });
        }
    }
}
