using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using DailyForWindows.ViewModels;

namespace DailyForWindows
{
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;

            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Visible = true,
                Text = "DailyForWindows"
            };

            var menu = new ContextMenuStrip();
            menu.Items.Add("History", null, (_, __) =>
            {
                var win = new Views.HistoryWindow { DataContext = _vm, Owner = this };
                win.ShowDialog();
            });
            menu.Items.Add("Settings", null, (_, __) =>
            {
                var win = new Views.SettingsWindow { DataContext = _vm, Owner = this };
                win.ShowDialog();
            });
            menu.Items.Add("Exit", null, (_, __) => Close());
            _notifyIcon.ContextMenuStrip = menu;

            Closing += OnClosing;

            Services.AutostartService.EnableAutostart();
        }

        private void OnClosing(object? sender, CancelEventArgs e)
        {
            _notifyIcon.Visible = false;
        }
    }
}
