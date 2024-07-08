using Microsoft.EntityFrameworkCore;
using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-V6M0EF7\\SQLEXPRESS;Initial Catalog=PrintManagement;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			//SeedingData(modelBuilder);
		}
		public async Task<int> CommitChangesAsync()
		{
			return await base.SaveChangesAsync();
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
			modelBuilder.Entity<Role>().HasData
				(
					new Role
					{
						Id = 0,
						RoleCode = "Admin",
						RoleName = "Admin",
					},
					new Role
					{
						Id = 1,
						RoleCode = "Employee",
						RoleName = "Employee",
					}
				);
		}
		#endregion
	}
}
