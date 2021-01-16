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

            //DataContext mit dem Viewmodel verbinden
            this.DataContext = new CubeViewModel();
        }

        public void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            //Das Fenster mit der Maus verschieben, wenn man in der obersten Leiste links gedrückt hält
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                var WindowMousePosition = Mouse.GetPosition(Application.Current.MainWindow);

                if (WindowMousePosition.Y <= 30)
                    this.DragMove();

            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            //Programm schließen
            Environment.Exit(0);
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            //Programm minimieren
            this.WindowState = WindowState.Minimized;
        }

        private void FullscreenWindow(object sender, RoutedEventArgs e)
        {
            //Fenster in Vollbildmodus machen und andersrum
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
    }
}
