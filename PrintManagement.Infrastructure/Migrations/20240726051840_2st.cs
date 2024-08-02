using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintManagement.Infrastructure.Migrations
{
    public partial class _2st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_User_EmployeeId",
                table: "Bill");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Bill",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 26, 12, 18, 39, 301, DateTimeKind.Local).AddTicks(643));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 26, 12, 18, 39, 301, DateTimeKind.Local).AddTicks(627));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 26, 12, 18, 39, 301, DateTimeKind.Local).AddTicks(645));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1aeae331-fdc1-4a2d-bd9d-a3cbf67165bd"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 12, 18, 39, 998, DateTimeKind.Local).AddTicks(675), new DateTime(2024, 7, 26, 12, 18, 39, 998, DateTimeKind.Local).AddTicks(688), "$2a$11$IPqesPiW0erPBhKgdS43buT2UrM6BZ0G/G6ki8fhdzIpvwT2QE1VK" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 12, 18, 39, 475, DateTimeKind.Local).AddTicks(1824), new DateTime(2024, 7, 26, 12, 18, 39, 475, DateTimeKind.Local).AddTicks(1840), "$2a$11$pg6N8f64mL/p0va.iF4bu.sCO2t1RFmE7EOwGsgDwrFgjR7jMrE32" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e2606ed3-4301-40fa-a3cb-887d95a1a8d4"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 12, 18, 39, 835, DateTimeKind.Local).AddTicks(8791), new DateTime(2024, 7, 26, 12, 18, 39, 835, DateTimeKind.Local).AddTicks(8809), "$2a$11$QhvhQV8qedd5uKrQxyuTB.lI2RiVUzfnpcQX7Rh29r73WxpmcGDFm" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 12, 18, 39, 648, DateTimeKind.Local).AddTicks(2982), new DateTime(2024, 7, 26, 12, 18, 39, 648, DateTimeKind.Local).AddTicks(3003), "$2a$11$6a0MMleI1vtiOR5/KJRe1uNoS7raw9pvSKxhlBUO/QD/hdwX/1wIS" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 12, 18, 39, 301, DateTimeKind.Local).AddTicks(668), new DateTime(2024, 7, 26, 12, 18, 39, 301, DateTimeKind.Local).AddTicks(669), "$2a$11$z7kHx4TYfP6A.nlDUfhP5.pjpLJoUFysWsVSg49zU9pG/NUF601e6" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_User_EmployeeId",
                table: "Bill",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_User_EmployeeId",
                table: "Bill");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Bill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 26, 10, 42, 8, 288, DateTimeKind.Local).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 26, 10, 42, 8, 288, DateTimeKind.Local).AddTicks(4453));

            migrationBuilder.UpdateData(
                table: "Team",
                keyColumn: "Id",
                keyValue: new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
                column: "CreateTime",
                value: new DateTime(2024, 7, 26, 10, 42, 8, 288, DateTimeKind.Local).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1aeae331-fdc1-4a2d-bd9d-a3cbf67165bd"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 10, 42, 8, 802, DateTimeKind.Local).AddTicks(1357), new DateTime(2024, 7, 26, 10, 42, 8, 802, DateTimeKind.Local).AddTicks(1366), "$2a$11$nOVxee/.aMBCg1LrfN5S1.tK9GaXrNxHuiV8HFU4bBXtJDcRbPr4G" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 10, 42, 8, 415, DateTimeKind.Local).AddTicks(9688), new DateTime(2024, 7, 26, 10, 42, 8, 415, DateTimeKind.Local).AddTicks(9696), "$2a$11$rSWlRZmz954ngoy4oLRGYOTKXlWAEhU3MgLRywkBFzTRHqMDJd6tK" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e2606ed3-4301-40fa-a3cb-887d95a1a8d4"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 10, 42, 8, 673, DateTimeKind.Local).AddTicks(9834), new DateTime(2024, 7, 26, 10, 42, 8, 673, DateTimeKind.Local).AddTicks(9844), "$2a$11$BfKzcjedjfxg07GtHtIWy.YMWChhwvsymd8UbPCosVyOrjPcrhZlO" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 10, 42, 8, 544, DateTimeKind.Local).AddTicks(1689), new DateTime(2024, 7, 26, 10, 42, 8, 544, DateTimeKind.Local).AddTicks(1701), "$2a$11$QvLQ0imetqF7KeXOxMYpS.N/iq3HU1SNl0F8x1AlqMe8BRXf837R6" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                columns: new[] { "CreateTime", "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 7, 26, 10, 42, 8, 288, DateTimeKind.Local).AddTicks(4493), new DateTime(2024, 7, 26, 10, 42, 8, 288, DateTimeKind.Local).AddTicks(4494), "$2a$11$ooqMpE6HNd18GP1K4l2rbuUPYFKPGaMPUKno5MMPnkLZYr7kFeIwq" });

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_User_EmployeeId",
                table: "Bill",
                column: "EmployeeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
