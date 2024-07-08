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
	public class ResourceForPrintJobConfig : IEntityTypeConfiguration<ResourceForPrintJob>
	{
		public void Configure(EntityTypeBuilder<ResourceForPrintJob> builder)
		{
			builder.ToTable("ResourceForPrintJob");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.PrintJobs).WithMany(x => x.ResourceForPrintJobs).HasForeignKey(x => x.PrintJobId);
			builder.HasOne(x => x.ResourcePropertyDetail).WithMany(x => x.ResourceForPrintJobs).HasForeignKey(x => x.ResourcePropertyDetailId);
		}
	}
}
