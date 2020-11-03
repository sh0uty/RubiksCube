using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
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

        private WindowState _CurWindowState;

        private ObservableCollection<String> _MovesDone;

        #endregion

        #region Public Members

        public ICommand RotateClockwiseCommand { get; private set; }
        public ICommand RotateCounterClockwiseCommand { get; private set; }
        public ICommand SaveCubeCommand { get; private set; }
        public ICommand LoadCubeCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand MinimizeWindowCommand { get; private set; }
        public ICommand FullscreenWindowCommand { get; private set; }

        public ObservableCollection<String> MovesDone
        {
            get { return _MovesDone; }
            set
            {
                _MovesDone.Add(value.ToString());
                OnPropertyChanged("MovesDone");
            }
        }

        public Cube RubiksCube
        {
            get { return _rubiksCube; }
            set { 
                if (_rubiksCube == value) 
                    return;
                _rubiksCube = value;
                OnPropertyChanged("RubiksCube");
            }
        }

        public WindowState CurWindowState
        {
            get { return _CurWindowState; }
            set
            {
                _CurWindowState = value;
                OnPropertyChanged("CurWindowState");
            }
        }

        #endregion

        #region Constructors

        public CubeViewModel()
        {
            _rubiksCube = new Cube();
            _MovesDone = new ObservableCollection<string>();

            RotateClockwiseCommand = new RelayCommand(RotateClockwise);
            RotateCounterClockwiseCommand = new RelayCommand(RotateCounterClockwise);
            LoadCubeCommand = new RelayCommand(LoadCube);
            SaveCubeCommand = new RelayCommand(SaveCube);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
            FullscreenWindowCommand = new RelayCommand(FullscreenWindow);

        }

        #endregion

        #region Command Functions

        private void RotateClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.Clockwise);
            MovesDone.Add(GetNotation((Orientation)parameter, Operation.Clockwise));
        }

        private void RotateCounterClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.CounterClockwise);
            MovesDone.Add(GetNotation((Orientation)parameter, Operation.CounterClockwise));
        }

        private void LoadCube(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Cube Template (*.ctmp)|*.ctmp";
            if (openFileDialog.ShowDialog() == true)
            {
                RubiksCube = LoadSaveCube.LoadCubeFromFile(openFileDialog.FileName);
                _MovesDone.Clear();            }
        }

        private void SaveCube(object parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Cube Template (*.ctmp)|*.ctmp";
            if (saveFileDialog.ShowDialog() == true)
            {
                LoadSaveCube.SaveCubeToFile(RubiksCube, saveFileDialog.FileName);
            }
        }

        private void CloseWindow(object parameter)
        {
            Environment.Exit(0);
        }

        private void MinimizeWindow(object parameter)
        {
            CurWindowState = WindowState.Minimized;
        }

        private void FullscreenWindow(object parameter)
        {
            if (CurWindowState == WindowState.Normal)
                CurWindowState = WindowState.Maximized;
            else
                CurWindowState = WindowState.Normal;
        }

        #endregion

        #region Helper Functions

        private string GetNotation(Orientation orientation, Operation operation)
        {
            string notation;

            switch (orientation)
            {

                case Orientation.Top:
                    notation = "U";
                    break;
                case Orientation.Left:
                    notation = "L";
                    break;
                case Orientation.Front:
                    notation = "U";
                    break;
                case Orientation.Right:
                    notation = "R";
                    break;
                case Orientation.Back:
                    notation = "B";
                    break;
                case Orientation.Down:
                    notation = "D";
                    break;

                default:
                    notation = "";
                    break;
            }

            if (operation == Operation.Clockwise)
                notation += " ";
            else
                notation += "' ";

            return notation;
        }

        #endregion
    }
}
