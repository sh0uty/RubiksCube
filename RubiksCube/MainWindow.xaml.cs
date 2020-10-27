using System.Windows;

namespace RubiksCube
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Cube cube = new Cube();

            this.DataContext = cube;

            cube.Randomize(20);
        }
    }
}
