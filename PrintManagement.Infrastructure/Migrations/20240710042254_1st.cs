using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintManagement.Infrastructure.Migrations
{
    public partial class _1st : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ịmage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    ResourceStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingMethodName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfMember = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourcePropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceProperty_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTIme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourcePropertyDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyDetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcePropertyDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcePropertyDetail_ResourceProperty_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "ResourceProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfirmEmail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfirmCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfirmEmail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyPerformanceIndicators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndicatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false),
                    ActuallyAchieved = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    AchieveKPI = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPerformanceIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyPerformanceIndicators_User_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDescriptionFromCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_User_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportCoupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResourcePropertyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportCoupon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportCoupon_ResourcePropertyDetail_ResourcePropertyDetailId",
                        column: x => x.ResourcePropertyDetailId,
                        principalTable: "ResourcePropertyDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportCoupon_User_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillStatus = table.Column<int>(type: "int", nullable: false),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // Sửa thành NoAction
                    table.ForeignKey(
                        name: "FK_Bill_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bill_User_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // Sửa thành NoAction
				});

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliverId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimateDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Delivery_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // Sửa thành NoAction
					table.ForeignKey(
                        name: "FK_Delivery_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Design",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesignTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesignStatus = table.Column<int>(type: "int", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Design", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Design_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Design_User_DesignerId",
                        column: x => x.DesignerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // Sửa thành NoAction
				});

            migrationBuilder.CreateTable(
                name: "PrintJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintJobStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintJobs_Design_DesignId",
                        column: x => x.DesignId,
                        principalTable: "Design",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceForPrintJob",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourcePropertyDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceForPrintJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceForPrintJob_PrintJobs_PrintJobId",
                        column: x => x.PrintJobId,
                        principalTable: "PrintJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceForPrintJob_ResourcePropertyDetail_ResourcePropertyDetailId",
                        column: x => x.ResourcePropertyDetailId,
                        principalTable: "ResourcePropertyDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { new Guid("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"), "Employee", "Employee" },
                    { new Guid("76186bc1-481a-4e21-aea2-0fce39afdf4f"), "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "Id", "CreateTime", "Description", "ManagerId", "Name", "NumberOfMember", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"), new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1406), "Phòng ban kỹ thuật", new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"), "Technical", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"), new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1392), "Phòng ban giao hàng", new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"), "Delivery", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"), new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1409), "Phòng ban kinh doanh", new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"), "Sales", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateTime", "DateOfBirth", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "TeamId", "UpdateTIme", "UserName" },
                values: new object[] { new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"), "data:image/png;base64,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", new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1425), new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1425), "abcd@gmail.com", "Nguyễn Nhật Mai", true, "mai", "1234567890", new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mai" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateTime", "DateOfBirth", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "TeamId", "UpdateTIme", "UserName" },
                values: new object[] { new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"), "data:image/png;base64,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", new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1422), new DateTime(2024, 7, 10, 11, 22, 54, 216, DateTimeKind.Local).AddTicks(1422), "abc@gmail.com", "Bùi Duy Đông", true, "abc", "1234567890", new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("4b605650-5edb-4441-a756-41c78fe72dbc"), new Guid("76186bc1-481a-4e21-aea2-0fce39afdf4f"), new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b") });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("d433b1f2-f9fb-40c3-876c-0154147fafae"), new Guid("76186bc1-481a-4e21-aea2-0fce39afdf4f"), new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a") });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerId",
                table: "Bill",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_EmployeeId",
                table: "Bill",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ProjectId",
                table: "Bill",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmEmail_UserId",
                table: "ConfirmEmail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_CustomerId",
                table: "Delivery",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_ProjectId",
                table: "Delivery",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_ShippingMethodId",
                table: "Delivery",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Design_DesignerId",
                table: "Design",
                column: "DesignerId");

            migrationBuilder.CreateIndex(
                name: "IX_Design_ProjectId",
                table: "Design",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportCoupon_EmployeeId",
                table: "ImportCoupon",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportCoupon_ResourcePropertyDetailId",
                table: "ImportCoupon",
                column: "ResourcePropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyPerformanceIndicators_EmployeeId",
                table: "KeyPerformanceIndicators",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrintJobs_DesignId",
                table: "PrintJobs",
                column: "DesignId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_EmployeeId",
                table: "Project",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceForPrintJob_PrintJobId",
                table: "ResourceForPrintJob",
                column: "PrintJobId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceForPrintJob_ResourcePropertyDetailId",
                table: "ResourceForPrintJob",
                column: "ResourcePropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceProperty_ResourceId",
                table: "ResourceProperty",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcePropertyDetail_PropertyId",
                table: "ResourcePropertyDetail",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TeamId",
                table: "User",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "ConfirmEmail");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "ImportCoupon");

            migrationBuilder.DropTable(
                name: "KeyPerformanceIndicators");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "ResourceForPrintJob");

            migrationBuilder.DropTable(
                name: "ShippingMethod");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "PrintJobs");

            migrationBuilder.DropTable(
                name: "ResourcePropertyDetail");

            migrationBuilder.DropTable(
                name: "Design");

            migrationBuilder.DropTable(
                name: "ResourceProperty");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
