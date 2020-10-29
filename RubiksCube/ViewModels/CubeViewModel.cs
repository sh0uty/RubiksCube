using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RubiksCube.Models;

namespace RubiksCube.ViewModels
{
    class CubeViewModel : INotifyPropertyChanged
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

        public ICommand RotateLeftCommand { get; set; }

        public Cube RubiksCube
        {
            get { return _rubiksCube; }
            private set { 
                if (_rubiksCube == value) return; 
                OnPropertyChanged(nameof(RubiksCube)); 
            }
        }

        #endregion

        #region Constructors

        public CubeViewModel()
        {
            _rubiksCube = new Cube();

            RotateLeftCommand = new RelayCommand(RotateLeft);
        }

        #endregion


        #region Private Methods

        private void RotateLeft()
        {
            RubiksCube.Rotate(Orientation.Left, Operation.Clockwise);
            OnPropertyChanged("RubiksCube");
        }

        #endregion
    }
}
