using System;
using System.Windows;
using System.Windows.Input;
using RubiksCube.ViewModels;

namespace RubiksCube.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CubeViewModel();
        }

        public void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var WindowMousePosition = Mouse.GetPosition(Application.Current.MainWindow);

                if (WindowMousePosition.Y <= 30)
                    this.DragMove();

            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void FullscreenWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
    }
}
