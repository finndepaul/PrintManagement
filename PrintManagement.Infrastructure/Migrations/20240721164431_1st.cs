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
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ResourcePropertyDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyDetailName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bill_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bill_User_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
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
                table: "Customer",
                columns: new[] { "Id", "Address", "Email", "FullName", "PhoneNumber" },
                values: new object[] { new Guid("9f0355cf-049d-4956-9720-4601c049ce3a"), "Xuân Phương, Nam Từ Liêm, Hà Nội", "dungnhanvaoday@gmail.com", "Nguyễn Văn A", "0322860999" });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "AvailableQuantity", "Image", "ResourceName", "ResourceStatus", "ResourceType" },
                values: new object[,]
                {
                    { new Guid("1fdf9a2b-daca-4d89-9efd-ba61769a29b3"), 200, null, "Văn phòng phẩm", 0, 0 },
                    { new Guid("de13c625-1024-4506-b7ac-ebef4716166c"), 8, null, "Máy móc", 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "RoleCode", "RoleName" },
                values: new object[,]
                {
                    { new Guid("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"), "Employee", "Employee" },
                    { new Guid("1f4cf3b6-135a-44df-964f-433c868beb9b"), "Manager", "Manager" },
                    { new Guid("76186bc1-481a-4e21-aea2-0fce39afdf4f"), "Admin", "Admin" },
                    { new Guid("a449f5f7-ac1e-4069-afc0-9f63c7ba9d9d"), "Leader", "Leader" }
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "Id", "CreateTime", "Description", "ManagerId", "Name", "NumberOfMember", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"), new DateTime(2024, 7, 21, 23, 44, 30, 487, DateTimeKind.Local).AddTicks(2176), "Phòng ban kỹ thuật", new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"), "Technical", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"), new DateTime(2024, 7, 21, 23, 44, 30, 487, DateTimeKind.Local).AddTicks(2157), "Phòng ban giao hàng", new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"), "Delivery", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"), new DateTime(2024, 7, 21, 23, 44, 30, 487, DateTimeKind.Local).AddTicks(2178), "Phòng ban kinh doanh", new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"), "Sales", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ResourceProperty",
                columns: new[] { "Id", "Quantity", "ResourceId", "ResourcePropertyName" },
                values: new object[,]
                {
                    { new Guid("c54debed-3b36-4a22-8433-cb0ba2926e6a"), 2, new Guid("1fdf9a2b-daca-4d89-9efd-ba61769a29b3"), "Ghim" },
                    { new Guid("ca09b1fc-bf5a-481b-b08f-ea42546420c8"), 2, new Guid("de13c625-1024-4506-b7ac-ebef4716166c"), "Máy In" },
                    { new Guid("e7721193-9620-4d6f-9e02-cdebc04057d0"), 5, new Guid("1fdf9a2b-daca-4d89-9efd-ba61769a29b3"), "Giấy" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Avatar", "CreateTime", "DateOfBirth", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "TeamId", "UpdateTime", "UserName" },
                values: new object[,]
                {
                    { new Guid("1aeae331-fdc1-4a2d-bd9d-a3cbf67165bd"), "data:image/png;base64,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", new DateTime(2024, 7, 21, 23, 44, 31, 16, DateTimeKind.Local).AddTicks(8804), new DateTime(2024, 7, 21, 23, 44, 31, 16, DateTimeKind.Local).AddTicks(8813), "blnor@gmail.com", "Black Noir", true, "$2a$11$7LsbqgspfTItEjhxGd6kzOBAatgy3G4KSDNEI/093tAOuNeb9wWyy", "0993517855", new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bla" },
                    { new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"), "data:image/png;base64,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", new DateTime(2024, 7, 21, 23, 44, 30, 625, DateTimeKind.Local).AddTicks(5058), new DateTime(2024, 7, 21, 23, 44, 30, 625, DateTimeKind.Local).AddTicks(5079), "Mvio11@gmail.com", "Maeve", true, "$2a$11$5YRA47/Wk4War8KZANoYpOLo913OWUKFrufxZxcdpJN6hVHRCQtgi", "0934567890", new Guid("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mae" },
                    { new Guid("e2606ed3-4301-40fa-a3cb-887d95a1a8d4"), "data:image/png;base64,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", new DateTime(2024, 7, 21, 23, 44, 30, 888, DateTimeKind.Local).AddTicks(4926), new DateTime(2024, 7, 21, 23, 44, 30, 888, DateTimeKind.Local).AddTicks(4933), "Atauu@gmail.com", "A-Train", true, "$2a$11$taqcX1jiojIbQK0pfcwiCeM9dJybKuMH2LOqL7lRB.sShji/OtiqC", "0933567855", new Guid("63a8c386-755e-4620-a58a-3a2c3126d28a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "atr" },
                    { new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"), "data:image/png;base64,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", new DateTime(2024, 7, 21, 23, 44, 30, 758, DateTimeKind.Local).AddTicks(5396), new DateTime(2024, 7, 21, 23, 44, 30, 758, DateTimeKind.Local).AddTicks(5404), "Hometowm@gmail.com", "Homelander", true, "$2a$11$47gLBXQz.z3eXu32EdmAh.OoeQ1ddIleZRJEwpJIIfwUEWI7xUdty", "0934567855", new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hom" },
                    { new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a"), "data:image/png;base64,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", new DateTime(2024, 7, 21, 23, 44, 30, 487, DateTimeKind.Local).AddTicks(2195), new DateTime(2024, 7, 21, 23, 44, 30, 487, DateTimeKind.Local).AddTicks(2196), "adminxyz@gmail.com", "Bùi Duy Đông", true, "$2a$11$nJFmA07XIKZa0rtlzNRp7ej4Ti9dwyyK.MyAMOwDhXwlfOwDGTXq.", "0934567891", new Guid("471c942c-c8dc-4909-90de-3b1f153212aa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dong" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("388c26af-4b47-453a-be1d-d749492b4226"), new Guid("1f4cf3b6-135a-44df-964f-433c868beb9b"), new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f") },
                    { new Guid("4b605650-5edb-4441-a756-41c78fe72dbc"), new Guid("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"), new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b") },
                    { new Guid("4c135b40-f998-4d43-bbcc-b293f3a84fea"), new Guid("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"), new Guid("fa315dae-ffb4-44b9-bfc2-362eeef18b5f") },
                    { new Guid("5651107f-2f6e-4c01-8aa5-03e970977193"), new Guid("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"), new Guid("e2606ed3-4301-40fa-a3cb-887d95a1a8d4") },
                    { new Guid("91d73cea-6402-4b96-8a56-77402b9d35f3"), new Guid("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"), new Guid("1aeae331-fdc1-4a2d-bd9d-a3cbf67165bd") },
                    { new Guid("a9580eca-3c2e-4db5-b137-60776730c65d"), new Guid("1f4cf3b6-135a-44df-964f-433c868beb9b"), new Guid("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b") },
                    { new Guid("d433b1f2-f9fb-40c3-876c-0154147fafae"), new Guid("76186bc1-481a-4e21-aea2-0fce39afdf4f"), new Guid("fe325627-f3e9-4e76-83e7-f91c45b8da3a") }
                });

            migrationBuilder.InsertData(
                table: "ResourcePropertyDetail",
                columns: new[] { "Id", "Image", "Price", "PropertyDetailName", "PropertyId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0aa2542b-4539-4b0d-b511-f60b2c42958c"), null, 1000m, "Giấy A0", new Guid("e7721193-9620-4d6f-9e02-cdebc04057d0"), 50 },
                    { new Guid("122c6e86-c4e1-48c3-99a4-a796864ee310"), null, 1000m, "Giấy A3", new Guid("e7721193-9620-4d6f-9e02-cdebc04057d0"), 50 },
                    { new Guid("3a249712-4a9a-47f5-ad8a-994e740095c4"), null, 1000m, "Giấy A1", new Guid("e7721193-9620-4d6f-9e02-cdebc04057d0"), 50 },
                    { new Guid("677a0524-2df7-4448-bc45-823509a90f32"), null, 500m, "Ghim giấy", new Guid("c54debed-3b36-4a22-8433-cb0ba2926e6a"), 100 },
                    { new Guid("6c668b7a-db39-46ba-b6f7-e7f6ae74854e"), null, 100m, "Ghim sắt", new Guid("c54debed-3b36-4a22-8433-cb0ba2926e6a"), 100 },
                    { new Guid("7f532022-02a3-493b-ac31-cc403987ac09"), null, 10000m, "Giấy Cứng in 3D", new Guid("e7721193-9620-4d6f-9e02-cdebc04057d0"), 30 },
                    { new Guid("a59b9038-076f-4b38-93ca-bc826886ca56"), null, 104000000m, "Máy in laser", new Guid("ca09b1fc-bf5a-481b-b08f-ea42546420c8"), 1 },
                    { new Guid("cb180ec9-355e-4660-bcb2-0ada02e6ecd1"), null, 24000000m, "Máy photocopy", new Guid("ca09b1fc-bf5a-481b-b08f-ea42546420c8"), 2 },
                    { new Guid("dd80d751-7b9f-4d20-84b8-0fcb3ff16934"), null, 45000000m, "Máy in 3D", new Guid("ca09b1fc-bf5a-481b-b08f-ea42546420c8"), 1 },
                    { new Guid("e21a4677-e8da-4280-a538-335707894949"), null, 1000m, "Giấy A2", new Guid("e7721193-9620-4d6f-9e02-cdebc04057d0"), 50 }
                });

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
