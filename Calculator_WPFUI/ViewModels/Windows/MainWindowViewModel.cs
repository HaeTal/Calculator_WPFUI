using Calculator_WPFUI.Views.Pages;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace Calculator_WPFUI.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Calculator_WPFUI";

        [ObservableProperty]    
        private object _dashboardPage;

        public MainWindowViewModel(DashboardPage dashboardPage)
        {
            DashboardPage = dashboardPage;
        }


        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Home",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage),
                
            },
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };
    }
}
