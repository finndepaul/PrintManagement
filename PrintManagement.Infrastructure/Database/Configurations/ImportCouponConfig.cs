using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Infrastructure.Database.Configurations
{
	public class ImportCouponConfig : IEntityTypeConfiguration<ImportCoupon>
	{
		public void Configure(EntityTypeBuilder<ImportCoupon> builder)
		{
			builder.ToTable("ImportCoupon");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.User).WithMany(x => x.ImportCoupons).HasForeignKey(x => x.EmployeeId);
			builder.HasOne(x => x.ResourcePropertyDetail).WithMany(x => x.ImportCoupons).HasForeignKey(x => x.ResourcePropertyDetailId);
		}
	}
}
