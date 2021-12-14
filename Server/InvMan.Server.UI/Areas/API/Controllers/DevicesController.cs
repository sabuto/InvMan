using Microsoft.AspNetCore.Mvc;
using System.Collections;
using InvMan.Server.Application;
using InvMan.Server.Domain.Models;

namespace InvMan.Server.UI.API.Controllers
{
	public class DevicesController : ApiController
	{
		private readonly IDevicesManager _manager;

		public DevicesController(IDevicesManager manager)
		{
			_manager = manager;
		}

		[HttpGet]
		public IEnumerable GetAppliances() =>
			_manager.GetAppliances();

		[HttpPut("{device}")]
		public void CreateDevice(Device device) =>
			_manager.CreateDevice(device);
	}
}
