using System.Windows;

namespace DailyForWindows.Views
{
    public partial class PromptWindow : Window
    {
        public PromptWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => InputBox.Focus();
            OkButton.Click += (s, e) =>
            {
                if (DataContext is ViewModels.MainViewModel vm)
                    vm.AddEntry(InputBox.Text);
                Close();
            };
            SnoozeButton.Click += (s, e) =>
            {
                if (DataContext is ViewModels.MainViewModel vm)
                    vm.Snooze();
                Close();
            };
        }
    }
}
