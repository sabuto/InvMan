using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvMan.Server.Domain.Models;

namespace InvMan.Server.Database.Configurations
{
	[ModelConfiguration]
	public class DeviceConfiguration : IEntityTypeConfiguration<Device>
	{
		public void Configure(EntityTypeBuilder<Device> builder)
		{
			builder.HasKey(d => d.ID);
			builder.Property(d => d.LocationID).IsRequired();
			builder.Property(d => d.TypeID).IsRequired();
			builder.HasIndex(d => d.InventoryNumber).IsUnique();
		}
	}
}
