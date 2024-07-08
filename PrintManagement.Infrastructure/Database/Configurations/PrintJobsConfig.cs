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
	public class PrintJobsConfig : IEntityTypeConfiguration<PrintJobs>
	{
		public void Configure(EntityTypeBuilder<PrintJobs> builder)
		{
			builder.ToTable("PrintJobs");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.Design).WithMany(x => x.PrintJobs).HasForeignKey(x => x.DesignId);
		}
	}
}
