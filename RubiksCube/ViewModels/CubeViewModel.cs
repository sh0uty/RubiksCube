using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RubiksCube.Models;

namespace RubiksCube.ViewModels
{
    public class CubeViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string fullpath = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fullpath));
        }

        #endregion

        #region Private Members

        private Cube _rubiksCube;

        #endregion

        #region Public Members

        public ICommand RotateClockwiseCommand { get; private set; }
        public ICommand RotateCounterClockwiseCommand { get; private set; }

        public ObservableCollection<string> MovesDone;

        public Cube RubiksCube
        {
            get { return _rubiksCube; }
            set { 
                if (_rubiksCube == value) 
                    return;
                _rubiksCube = value;
            }
        }

        #endregion

        #region Constructors

        public CubeViewModel()
        {
            _rubiksCube = new Cube();

            MovesDone = new ObservableCollection<string>();

            RotateClockwiseCommand = new RelayCommand(RotateClockwise);
            RotateCounterClockwiseCommand = new RelayCommand(RotateCounterClockwise);
        }

        #endregion


        #region Private Methods

        private void RotateClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.Clockwise);
            OnPropertyChanged("RubiksCube");
        }

        private void RotateCounterClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.CounterClockwise);
            OnPropertyChanged("RubiksCube");
        }

        #endregion
    }
}
