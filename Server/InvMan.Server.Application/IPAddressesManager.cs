using System.Linq;
using InvMan.Server.Database;
using InvMan.Server.Domain;

namespace InvMan.Server.Application
{
	public class IPAddressesManager : IIPAddressesManager
	{
		private readonly IIPAddressRepository _ipRepo;

		public IPAddressesManager(IIPAddressRepository ipRepo) =>
			_ipRepo = ipRepo;

		public IQueryable<string> GetFreeIP(int amount) =>
			_ipRepo.IPAddresses.Take(amount).
				Where(ip => ip.DeviceID == -1).
					Select(ip => ip.Address);

	}
}