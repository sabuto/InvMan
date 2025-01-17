using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvMan.Server.Domain.Models;

namespace InvMan.Server.Database.Configurations
{
	[ModelConfiguration]
	public class IPAddressConfiguration : IEntityTypeConfiguration<IPAddress>
	{
		public void Configure(EntityTypeBuilder<IPAddress> builder)
		{
			builder.HasKey(ia => ia.ID);
			builder.HasIndex(ia => ia.Address).IsUnique();
		}
	}
}
