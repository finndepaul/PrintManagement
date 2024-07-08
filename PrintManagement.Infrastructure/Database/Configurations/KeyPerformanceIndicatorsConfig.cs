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
	public class KeyPerformanceIndicatorsConfig : IEntityTypeConfiguration<KeyPerformanceIndicators>
	{
		public void Configure(EntityTypeBuilder<KeyPerformanceIndicators> builder)
		{
			builder.ToTable("KeyPerformanceIndicators");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.User).WithMany(x => x.KeyPerformanceIndicators).HasForeignKey(x => x.EmployeeId);
		}
	}
}
