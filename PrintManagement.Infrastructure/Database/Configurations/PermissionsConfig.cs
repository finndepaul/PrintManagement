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
	public class PermissionsConfig : IEntityTypeConfiguration<Permissions>
	{
		public void Configure(EntityTypeBuilder<Permissions> builder)
		{
			builder.ToTable("Permissions");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			//
			builder.HasOne(x => x.User).WithMany(x => x.Permissions).HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.Role).WithMany(x => x.Permissions).HasForeignKey(x => x.RoleId);
		}
	}
}
