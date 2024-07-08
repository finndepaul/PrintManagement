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
	public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
	{
		public void Configure(EntityTypeBuilder<Delivery> builder)
		{
			builder.ToTable("Delivery");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.ShippingMethod).WithMany(x => x.Deliveries).HasForeignKey(x => x.ShippingMethodId);
			builder.HasOne(x => x.Customer).WithMany(x => x.Deliveries).HasForeignKey(x => x.CustomerId); // sửa lại thành no Action ở migration
			builder.HasOne(x => x.Project).WithMany(x => x.Deliveries).HasForeignKey(x => x.ProjectId);
		}
	}
}
