using System.Linq;
using System.Windows;

namespace DailyForWindows.Views
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
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
