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
	public class ShippingMethodConfig : IEntityTypeConfiguration<ShippingMethod>
	{
		public void Configure(EntityTypeBuilder<ShippingMethod> builder)
		{
			builder.ToTable("ShippingMethod");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
		}
	}
}
