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
	public class ResourcePropertyConfig : IEntityTypeConfiguration<ResourceProperty>
	{
		public void Configure(EntityTypeBuilder<ResourceProperty> builder)
		{
			builder.ToTable("ResourceProperty");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.Resources).WithMany(x => x.ResourceProperties).HasForeignKey(x => x.ResourceId);
		}
	}
}
