﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InvMan.Common.SDK;
using InvMan.Desktop.Service;
using InvMan.Common.SDK.Models;
using ReactiveUI;

namespace InvMan.Desktop.UI.ViewModels
{
    public class DevicesListViewModel : ViewModelBase, IDevicesListViewModel
    {
        private readonly IApplicationEvents _appEvents;

        private readonly IDevicesProvider _devicesProvider;

        private readonly IUserSession _session;

        private Appliance _selectedAppliance;

        private IEnumerable<Appliance> _devicesCache;

        private string _noAppliancesMessage;

        private bool _areThereAppliances;

        private bool _areAppliancesLoaded;

        public DevicesListViewModel(
            IDevicesProvider devicesProvider,
            IApplicationEvents appEvents,
            IUserSession session
        )
        {
            _appEvents = appEvents;
            _session = session;
            _devicesProvider = devicesProvider;
            _areAppliancesLoaded = false;
            _devicesCache = new List<Appliance>();

            Appliances = new ObservableCollection<Appliance>();
        }

        public ObservableCollection<Appliance> Appliances { get; set; }

        public IEnumerable<Appliance> CachedDevices => _devicesCache;

        public Appliance SelectedAppliance
        {
            get => _selectedAppliance;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedAppliance, value);

                _appEvents.RaiseApplianceSelected(_selectedAppliance);
            }
        }

        public bool AreAppliancesLoaded
        {
            get => _areAppliancesLoaded;
            set => this.RaiseAndSetIfChanged(ref _areAppliancesLoaded, value);
        }

        public bool AreThereAppliances
        {
            get => _areThereAppliances;
            set { this.RaiseAndSetIfChanged(ref _areThereAppliances, value); }
        }

        public string NoAppliancesMessage
        {
            get => _noAppliancesMessage;
            set { this.RaiseAndSetIfChanged(ref _noAppliancesMessage, value); }
        }

        public void LoadAppliances(IEnumerable<Appliance> devices)
        {
            Appliances.Clear();

            foreach (var device in devices)
                Appliances.Add(device);

            if (Appliances.Count == 0) {
                AreThereAppliances = false;
                NoAppliancesMessage = "Устройства не найдены";
            }
            else AreThereAppliances = true;

        }

        private async Task LoadAppliances()
        {
            AreAppliancesLoaded = false;

            _devicesCache = await _devicesProvider.GetDevicesAsync(_session.AccessToken);
            foreach (var device in _devicesCache)
                Appliances.Add(device);
        }

        public async void InitializeList()
        {
            try
            {
                await LoadAppliances();

                if (Appliances.Count > 0) {
                    AreThereAppliances = true;
                    SelectedAppliance = Appliances[0];
                }
                else {
                    AreThereAppliances = false;
                    NoAppliancesMessage = "Нет устройств";
                }
            }
            catch (ArgumentException)
            {
                AreThereAppliances = false;
                NoAppliancesMessage = "Ошибка доступа";
            }
            catch
            {
                AreThereAppliances = false;
                NoAppliancesMessage = "Что-то пошло не так";
            }
            finally { AreAppliancesLoaded = true; }
        }
    }
}