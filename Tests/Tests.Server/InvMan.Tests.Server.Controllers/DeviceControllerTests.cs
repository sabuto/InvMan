using Xunit;
using Moq;
using InvMan.Server.UI.API.Controllers;
using InvMan.Server.Domain;

namespace InvMan.Tests.Server.Controllers
{
	public class DeviceControllerTests
	{
		private readonly DevicesController _controller;

		public DeviceControllerTests()
		{
			var mock = new Mock<IDeviceRepository>();
			// mock.Setup(
			// 	repo => repo.AllDevices();
			// );

			_controller = new DevicesController(mock.Object);
		}

		[Fact]
		public void AreDevicesReturnProperly()
		{

		}
	}
}
