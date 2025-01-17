using System;
using System.Collections.Generic;
using InvMan.Common.SDK.Models;
using InvMan.Server.Domain.Models;

namespace InvMan.Server.Application
{
	public interface IDevicesManager
	{
		void CreateDevice(string networkName, string inventoryNumber, string type);

		Device GetDeviceByID(Guid deviceID);

		IEnumerable<Device> GetDevices();

		IEnumerable<Appliance> GetAppliances();
	}
}
