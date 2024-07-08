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
	public class DesignConfig : IEntityTypeConfiguration<Design>
	{
		public void Configure(EntityTypeBuilder<Design> builder)
		{
			builder.ToTable("Design");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.User).WithMany(x => x.Designs).HasForeignKey(x => x.DesignerId);
			builder.HasOne(x => x.Project).WithMany(x => x.Designs).HasForeignKey(x => x.ProjectId);
		}
	}
}
