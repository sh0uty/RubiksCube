using System.Windows;

namespace RubiksCube
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Cube cube;
        public MainWindow()
        {
            InitializeComponent();

            cube = new Cube();

            this.DataContext = cube;
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {
            cube.Rotate(Orientation.Left, Operation.Clockwise);
        }
    }
}
