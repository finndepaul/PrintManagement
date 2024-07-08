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
	public class ResourcesConfig : IEntityTypeConfiguration<Resources>
	{
		public void Configure(EntityTypeBuilder<Resources> builder)
		{
			builder.ToTable("Resources");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
		}
	}
}
