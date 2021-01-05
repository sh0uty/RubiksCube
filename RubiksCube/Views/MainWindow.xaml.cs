using System.Windows;
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
    }
}
