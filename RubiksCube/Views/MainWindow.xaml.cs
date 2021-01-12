using System.Windows;
using System.Windows.Input;
using RubiksCube.ViewModels;

namespace RubiksCube.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
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
    }
}
