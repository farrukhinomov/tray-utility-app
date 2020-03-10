using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows;

namespace UtilitiesHandler
{
    public class MainViewModel : ReactiveObject
    {
        public ObservableCollection<Utility> Utilities { get; } = new ObservableCollection<Utility>();
        public string OutputFolder { get; private set; }

        private readonly IUtilityService _utilityService;
        private readonly IWindowsService _windowsService;
        public ITrayService TrayService { get; }

        public MainViewModel(IUtilityService utilityService, IWindowsService windowsService, ITrayService trayService)
        {
            _utilityService = utilityService;
            _windowsService = windowsService;
            TrayService = trayService;
        }

        public void ViewAppearing()
        {
            Utilities.Clear();
            var items = _utilityService.GetUtilities();
            Utilities.AddRange(items);
            Utilities.Select(x => x.Changed).Merge().Select(x => Unit.Default).Subscribe(ItemPropertyChanged);
        }
        private void ItemPropertyChanged(Unit unit)
        {
            _utilityService.SaveDisabledUtilitiesNameToFile(Utilities);
            TrayService.RefreshTrayItems();
        }

        private WindowState _windowState;

        public WindowState WindowState
        {
            get { return _windowState; }
            set => this.RaiseAndSetIfChanged(ref _windowState, value);
        }

    }
}
