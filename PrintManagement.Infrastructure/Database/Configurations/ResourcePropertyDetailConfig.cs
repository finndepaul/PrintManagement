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
	public class ResourcePropertyDetailConfig : IEntityTypeConfiguration<ResourcePropertyDetail>
	{
		public void Configure(EntityTypeBuilder<ResourcePropertyDetail> builder)
		{
			builder.ToTable("ResourcePropertyDetail");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.ResourceProperty).WithMany(x => x.ResourcePropertyDetails).HasForeignKey(x => x.PropertyId);
		}
	}
}
