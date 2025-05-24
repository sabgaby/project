using System.Windows;

namespace DailyForWindows.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            SaveButton.Click += (s, e) =>
            {
                if (DataContext is ViewModels.MainViewModel vm)
                {
                    vm.SaveSettings(vm.Settings);
                    Close();
                }
            };
        }
    }
}
