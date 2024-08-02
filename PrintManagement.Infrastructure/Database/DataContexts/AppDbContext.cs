using Microsoft.EntityFrameworkCore;
using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PrintManagement.Infrastructure.Database.DataContexts
{
	public class AppDbContext : DbContext, IAppDbContext
	{
        public AppDbContext()
        {
            
        }

		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			SeedingData(modelBuilder);
		}
		public async Task<int> CommitChangesAsync(CancellationToken cancellationToken)
		{
			return await base.SaveChangesAsync(cancellationToken);
		}

		public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}

		#region DbSet
		public DbSet<Bill> Bills { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Delivery> Deliveries { get; set; }
		public DbSet<Design> Designs { get; set; }
		public DbSet<ImportCoupon> ImportCoupons { get; set; }
		public DbSet<KeyPerformanceIndicators> KeyPerformanceIndicators { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Permissions> Permissions { get; set; }
		public DbSet<PrintJobs> PrintJobs { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<ResourceForPrintJob> ResourceForPrintJobs { get; set; }
		public DbSet<ResourceProperty> ResourceProperties { get; set; }
		public DbSet<ResourcePropertyDetail> ResourcePropertyDetails { get; set; }
		public DbSet<Resources> Resources { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<ShippingMethod> ShippingMethods { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<User> Users { get; set; }
		#endregion

		#region Seeding Data
		private static void SeedingData(ModelBuilder modelBuilder)
		{
			// Role
			modelBuilder.Entity<Role>().HasData
				(
					new Role
					{
						Id = Guid.Parse("76186bc1-481a-4e21-aea2-0fce39afdf4f"),
						RoleCode = "Admin",
						RoleName = "Admin",
					},
					new Role
					{
						Id = Guid.Parse("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"),
						RoleCode = "Employee",
						RoleName = "Employee",
					},
					new Role
					{
						Id = Guid.Parse("1f4cf3b6-135a-44df-964f-433c868beb9b"),
						RoleCode = "Manager",
						RoleName = "Manager",
					},
					new Role
					{
						Id = Guid.Parse("a449f5f7-ac1e-4069-afc0-9f63c7ba9d9d"),
						RoleCode = "Leader",
						RoleName = "Leader",
					}
				);
			// Team
			modelBuilder.Entity<Team>().HasData
				(
					new Team
					{
						Id = Guid.Parse("63a8c386-755e-4620-a58a-3a2c3126d28a"),
						CreateTime = DateTime.Now,
						Description = "Phòng ban giao hàng",
						Name = "Delivery",
						NumberOfMember = 0,
						ManagerId = Guid.Parse("fa315dae-ffb4-44b9-bfc2-362eeef18b5f")
					},
					new Team
					{
						Id = Guid.Parse("471c942c-c8dc-4909-90de-3b1f153212aa"),
						CreateTime = DateTime.Now,
						Description = "Phòng ban kỹ thuật",
						Name = "Technical",
						NumberOfMember = 0,
						ManagerId = Guid.Parse("fa315dae-ffb4-44b9-bfc2-362eeef18b5f")
					},
					new Team
					{
						Id = Guid.Parse("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
						CreateTime = DateTime.Now,
						Description = "Phòng ban kinh doanh",
						Name = "Sales",
						NumberOfMember = 0,
						ManagerId = Guid.Parse("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b")
					}

				);
			// User
			modelBuilder.Entity<User>().HasData
				(
					new User
					{
						Avatar = "data:image/png;base64,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",
						Id = Guid.Parse("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
						IsActive = true,
						CreateTime = DateTime.Now,
						DateOfBirth = DateTime.Now,
						FullName = "Bùi Duy Đông",
						Email = "adminxyz@gmail.com",
						UserName = "dong",
						Password = BCryptNet.HashPassword("abc"),
						PhoneNumber = "0934567891",
						TeamId = Guid.Parse("471c942c-c8dc-4909-90de-3b1f153212aa"),
					},
					new User
					{
						Avatar = "data:image/png;base64,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",
						Id = Guid.Parse("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
						IsActive = true,
						CreateTime = DateTime.Now,
						DateOfBirth = DateTime.Now,
						FullName = "Maeve",
						Email = "Mvio11@gmail.com",
						UserName = "mae",
						Password = BCryptNet.HashPassword("abc"),
						PhoneNumber = "0934567890",
						TeamId = Guid.Parse("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"),
					},
                    new User
                    {
                        Avatar = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMwAAADACAMAAAB/Pny7AAAAb1BMVEX///8uNDaHiYoiKiyhoqQAAAAnLjArMTMAExecnZ719fX7+/v4+PgfJinv7+9wc3QPGh17fX6PkZIaIiXg4eHb29xfY2Tm5+fU1dVNUVOqq6w6P0G6u7sVHiE0OTvJyspFSksACg9oa2xWWlsAAAlKDmu9AAAGYklEQVR4nO2d2XqjOBBGDTIS2GbfN4PB7/+MA/F4Ynd7wVJBVTKcu1zka/5Iqk2l6s1mZWVlZeV/RlQVuu+7ruvr2ybF/hoF4m1SHjWNc8Y441xoxzKxIuyvkiHVbTPkQgjtP4YfeGiK4Ict0KFpDe9Gxi3CM/LmgP2Fk3Gq0mCPlVxgRls52F85CafqTy+lfMkxk2qH/aXvyXQ7fCdlJLT1GPtb31Hkz87K32enL7C/9iWHQLOnSRmxtWCP/cXPSbuQT9cyeKDQJWsH4vyDZbkQJkTVSGgZtlpP0oVm7SQr9ic1RTVpUsto0TSP3rlxfE9Oy6BGJ+Y+91tpLZqoC1oWutE+ssn3sLLB/v5bnFzq8F+pE0obLTBVtGiaucVW8E1sTozHniE8Ohatl/CW99QutoYrmaGqRdMMKkuTK1iyK3aHreJCJun67/FoRDU+wMIMzibA1jHitCBieE4hDCg0Rbt8QRwphAHu21LMNLiOrWSz2SVAYhiBmCaDMMwjPM+wtWyKI8iRGQ8NfuUpANplFIzz3lcK/m8JfWzj7HTKQeYVG70YEEEZs0FMgn0RBWbMhjPTY5uzBk4Mz7FjgAYmMvsS065iVjFPxACeGXQxgNYMPziL4fwMS7AvOVPACKDDLgP8qtjsV0XNvyufgcs00S0zYA3AJlADAKvOMB9byUAFc2jEscJWMrADqmi2JHrQdJB9xgjUAAciELcZYufM/wIR0VC5n9lEEDdnRBZmWBrl6yY6d5qb9KR623zGDphvsM5qYgxCfQAbp1Uyz3aOXcu8I/MUPCf3sPPle/bF1KbZvyHX1bRxfFtSjbADUptsJJJ1ncwlZMmuZHIdNPi1/4fEMmrsHru+9ISo/zgSqHuS6zLidB9GaUZH7ux/cwiMD2yaMHQSCdlTqtPk7CY8UUiUX+J0IZ+wOoLyo4Ybqp6zN3IEEzn5ZbngWIl49faEh1qy/QnLciEt3NJ7HN8I2yv94udIGXEqq9PM+n6/CVabx876CU/n/iRqCj3RvPPJGzHNs6cletGQ9ZLvcKKsqQorCAJrW1RNFv2s7fWA/WG32x0OxHKWlZWVlZWVlZVfzX6/d+IhrCy2EymG0DN2hl/D/vJ7dk5UWW5rGoZxMqdzOo2/UPrbJnWIlGnSuNLbk2F+NqThNok2jboPhkXCVuJkhWufa9W2BsG889EvMkw9caG3ZgjUojWsUKsXWHloFiS28prc66lZYmEU0jM9r8HaM7+xwz5YWk6qtzboonzDWLvoVdphW7KZpIxwVi53yxnnKnfLk+R4C8082Vmv533BYBvFAqXCtAVo+5mCkcy9OIeGgTVlv6PWmllPjrOd+7TcwsI5C+ypLtu5IIew5xuGGLnvLpHA1TB/JjVRsrSWUc08/Rup+gwTGTX2HLfrO5npZRCE8G31h1x+spQiZgdtoTvFAUYqGMCvBIKF3P4TNaAva6oTppZhpwE+rcmOC/r9R/ASLGFL4Z4vysJcKJMWTGmGmRchgDqfmxJ5k40AvUhL3cWC/leEPkQkUKF5y3s8gDaoLEc//ReY+jvug3XCVnHlZKlW12OBbsmu8KOiszmoDpWExLTUIs5o2Tz5NYKpldUpLcx4alS07KDuK2AQtso+qxSfX0FzVvE1JREfc4W18lpS1JTsEQqDUAOQcZ+QePImgEK4fA9vZbVESMWlV9iyNcEtseM/wmRrGx1BMVzyETTQCAZYZEfUNiUp939BlHJZDdh4HEhkR+3AzMaAhssNQYKbjwWJLWUBHPzS3yNYIpM8xz1BYzZO25Rxm4CjCyGRG7ZX0YvMRuRKmyQt8+hoZBI0oEHs0Mg5GovkLhv+wjL3AUTFaFKXG1TFcJlkcxWzAKuYVcwCrGJWMfMj1xHwq2Kzimg+08pEzYDTyyGRS86chGZBQ67L0adZ0JBrCyRpAWSLgBHFIgBvJe80KN4CMNlRqA1BCxBKt53R8zQ8l9WyaejdNiu0afUkmgC/qVWmOjuyD5bngQulHtqKkhouf/q/2Ft0WoE4U+w32+wCKmq4HSgPDdgFc737/QzOA4Cm851FQQ1jW5AG+kMl0BuC6mMFNZgiTQzUxeGwY10b5mG9bhDc1KDHhle5JhhfVpHg4z/Yz/Gf7MRWl7elYEvBRdn27na2uQ1pU1mBvhCBVTXo41tWVlZWEPgHF9WLRrafYeAAAAAASUVORK5CYII=",
                        Id = Guid.Parse("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"),
                        IsActive = true,
                        CreateTime = DateTime.Now,
                        DateOfBirth = DateTime.Now,
                        FullName = "Homelander",
                        Email = "Hometowm@gmail.com",
                        UserName = "hom",
                        Password = BCryptNet.HashPassword("abc"),
                        PhoneNumber = "0934567855",
                        TeamId = Guid.Parse("471c942c-c8dc-4909-90de-3b1f153212aa"),
                    },
                    new User
                    {
                        Avatar = "data:image/png;base64,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",
                        Id = Guid.Parse("e2606ed3-4301-40fa-a3cb-887d95a1a8d4"),
                        IsActive = true,
                        CreateTime = DateTime.Now,
                        DateOfBirth = DateTime.Now,
                        FullName = "A-Train",
                        Email = "Atauu@gmail.com",
                        UserName = "atr",
                        Password = BCryptNet.HashPassword("abc"),
                        PhoneNumber = "0933567855",
                        TeamId = Guid.Parse("63a8c386-755e-4620-a58a-3a2c3126d28a"),
                    },
                    new User
                    {
                        Avatar = "data:image/png;base64,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",
                        Id = Guid.Parse("1aeae331-fdc1-4a2d-bd9d-a3cbf67165bd"),
                        IsActive = true,
                        CreateTime = DateTime.Now,
                        DateOfBirth = DateTime.Now,
                        FullName = "Black Noir",
                        Email = "blnor@gmail.com",
                        UserName = "bla",
                        Password = BCryptNet.HashPassword("abc"),
                        PhoneNumber = "0993517855",
                        TeamId = Guid.Parse("471c942c-c8dc-4909-90de-3b1f153212aa"),
                    }
                );
			// Permissions
			modelBuilder.Entity<Permissions>().HasData
				(
					new Permissions
					{
						Id = Guid.Parse("d433b1f2-f9fb-40c3-876c-0154147fafae"),
                        UserId = Guid.Parse("fe325627-f3e9-4e76-83e7-f91c45b8da3a"),
                        RoleId = Guid.Parse("76186bc1-481a-4e21-aea2-0fce39afdf4f"),
					},
					new Permissions
					{
						Id = Guid.Parse("4b605650-5edb-4441-a756-41c78fe72dbc"),
                        UserId = Guid.Parse("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                        RoleId = Guid.Parse("128AD738-8D2C-4A0E-B2FF-0A72BEF4C106"),
						
					},
                    new Permissions
                    {
                        Id = Guid.Parse("a9580eca-3c2e-4db5-b137-60776730c65d"),
                        UserId = Guid.Parse("e17b68f6-3f3e-4a75-a3fa-c9d17144d13b"),
                        RoleId = Guid.Parse("1f4cf3b6-135a-44df-964f-433c868beb9b"),
                    },
					new Permissions
                    {
                        Id = Guid.Parse("4c135b40-f998-4d43-bbcc-b293f3a84fea"),
                        UserId = Guid.Parse("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"),
                        RoleId = Guid.Parse("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"),
                    },
                    new Permissions
                    {
                        Id = Guid.Parse("388c26af-4b47-453a-be1d-d749492b4226"),
                        UserId = Guid.Parse("fa315dae-ffb4-44b9-bfc2-362eeef18b5f"),
                        RoleId = Guid.Parse("1f4cf3b6-135a-44df-964f-433c868beb9b"),
                    },
                    new Permissions
                    {
                        Id = Guid.Parse("5651107f-2f6e-4c01-8aa5-03e970977193"),
                        UserId = Guid.Parse("e2606ed3-4301-40fa-a3cb-887d95a1a8d4"),
                        RoleId = Guid.Parse("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"),
                    },
                    new Permissions
                    {
                        Id = Guid.Parse("91d73cea-6402-4b96-8a56-77402b9d35f3"),
                        UserId = Guid.Parse("1aeae331-fdc1-4a2d-bd9d-a3cbf67165bd"),
                        RoleId = Guid.Parse("128ad738-8d2c-4a0e-b2ff-0a72bef4c106"),
                    }
                );
            // Customer
            modelBuilder.Entity<Customer>().HasData
                (
                    new Customer
                    {
                        Id = Guid.Parse("9f0355cf-049d-4956-9720-4601c049ce3a"),
                        FullName = "Nguyễn Văn A",
						PhoneNumber = "0322860999",
						Address = "Xuân Phương, Nam Từ Liêm, Hà Nội",
						Email = "dungnhanvaoday@gmail.com"
                    }
                );
            // Resources
            modelBuilder.Entity<Resources>().HasData
                (
                    new Resources
                    {
                        Id = Guid.Parse("1fdf9a2b-daca-4d89-9efd-ba61769a29b3"),
						ResourceName = "Văn phòng phẩm",
						Image = null,
						ResourceType = Domain.Enumerates.ResourceType.Stationery,
                        AvailableQuantity = 200,
						ResourceStatus = Domain.Enumerates.ResourceStatus.Ready,
                    },
                    new Resources
                    {
                        Id = Guid.Parse("de13c625-1024-4506-b7ac-ebef4716166c"),
                        ResourceName = "Máy móc",
                        Image = null,
                        ResourceType = Domain.Enumerates.ResourceType.Machine,
                        AvailableQuantity = 8,
                        ResourceStatus = Domain.Enumerates.ResourceStatus.Ready,
                    }
                );
            // ResourceProperty
            modelBuilder.Entity<ResourceProperty>().HasData
                (
                    new ResourceProperty
                    {
                        Id = Guid.Parse("e7721193-9620-4d6f-9e02-cdebc04057d0"),
                        ResourcePropertyName = "Giấy",
						ResourceId = Guid.Parse("1fdf9a2b-daca-4d89-9efd-ba61769a29b3"),
						Quantity =  5,
                    },
                    new ResourceProperty
                    {
                        Id = Guid.Parse("c54debed-3b36-4a22-8433-cb0ba2926e6a"),
                        ResourcePropertyName = "Ghim",
                        ResourceId = Guid.Parse("1fdf9a2b-daca-4d89-9efd-ba61769a29b3"),
                        Quantity = 2,
                    },
                    new ResourceProperty
                    {
                        Id = Guid.Parse("ca09b1fc-bf5a-481b-b08f-ea42546420c8"),
                        ResourcePropertyName = "Máy In",
                        ResourceId = Guid.Parse("de13c625-1024-4506-b7ac-ebef4716166c"),
                        Quantity = 2,
                    }
                );
            // ResourcePropertyDetail
            modelBuilder.Entity<ResourcePropertyDetail>().HasData
                (
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("0aa2542b-4539-4b0d-b511-f60b2c42958c"),
                        PropertyId = Guid.Parse("e7721193-9620-4d6f-9e02-cdebc04057d0"),
						PropertyDetailName = "Giấy A0",
						Image = null,
						Price = decimal.Parse("1.000"),
						Quantity = 50
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("3a249712-4a9a-47f5-ad8a-994e740095c4"),
                        PropertyId = Guid.Parse("e7721193-9620-4d6f-9e02-cdebc04057d0"),
                        PropertyDetailName = "Giấy A1",
                        Image = null,
                        Price = decimal.Parse("1.000"),
                        Quantity = 50
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("e21a4677-e8da-4280-a538-335707894949"),
                        PropertyId = Guid.Parse("e7721193-9620-4d6f-9e02-cdebc04057d0"),
                        PropertyDetailName = "Giấy A2",
                        Image = null,
                        Price = decimal.Parse("1.000"),
                        Quantity = 50
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("122c6e86-c4e1-48c3-99a4-a796864ee310"),
                        PropertyId = Guid.Parse("e7721193-9620-4d6f-9e02-cdebc04057d0"),
                        PropertyDetailName = "Giấy A3",
                        Image = null,
                        Price = decimal.Parse("1.000"),
                        Quantity = 50
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("7f532022-02a3-493b-ac31-cc403987ac09"),
                        PropertyId = Guid.Parse("e7721193-9620-4d6f-9e02-cdebc04057d0"),
                        PropertyDetailName = "Giấy Cứng in 3D",
                        Image = null,
                        Price = decimal.Parse("10.000"),
                        Quantity = 30
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("677a0524-2df7-4448-bc45-823509a90f32"),
                        PropertyId = Guid.Parse("c54debed-3b36-4a22-8433-cb0ba2926e6a"),
                        PropertyDetailName = "Ghim giấy",
                        Image = null,
                        Price = decimal.Parse("500"),
                        Quantity = 100
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("6c668b7a-db39-46ba-b6f7-e7f6ae74854e"),
                        PropertyId = Guid.Parse("c54debed-3b36-4a22-8433-cb0ba2926e6a"),
                        PropertyDetailName = "Ghim sắt",
                        Image = null,
                        Price = decimal.Parse("100"),
                        Quantity = 100
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("cb180ec9-355e-4660-bcb2-0ada02e6ecd1"),
                        PropertyId = Guid.Parse("ca09b1fc-bf5a-481b-b08f-ea42546420c8"),
                        PropertyDetailName = "Máy photocopy",
                        Image = null,
                        Price = decimal.Parse("24.000.000"),
                        Quantity = 2
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("a59b9038-076f-4b38-93ca-bc826886ca56"),
                        PropertyId = Guid.Parse("ca09b1fc-bf5a-481b-b08f-ea42546420c8"),
                        PropertyDetailName = "Máy in laser",
                        Image = null,
                        Price = decimal.Parse("104.000.000"),
                        Quantity = 1
                    },
                    new ResourcePropertyDetail
                    {
                        Id = Guid.Parse("dd80d751-7b9f-4d20-84b8-0fcb3ff16934"),
                        PropertyId = Guid.Parse("ca09b1fc-bf5a-481b-b08f-ea42546420c8"),
                        PropertyDetailName = "Máy in 3D",
                        Image = null,
                        Price = decimal.Parse("45.000.000"),
                        Quantity = 1
                    }
                );

            // shipping Method
            modelBuilder.Entity<ShippingMethod>().HasData
                (
                    new ShippingMethod
                    {
                        Id = Guid.Parse("2ddfd76b-d034-4d48-be98-b651d59d1538"),
                        ShippingMethodName = "Nhanh",
                    }
                );
            
        }
        #endregion
    }
}
