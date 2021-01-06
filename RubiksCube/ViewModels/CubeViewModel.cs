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

        #endregion

        #region Public Members

        //ICommands für die Buttons
        public ICommand RotateClockwiseCommand { get; private set; }
        public ICommand RotateCounterClockwiseCommand { get; private set; }
        public ICommand SaveCubeCommand { get; private set; }
        public ICommand LoadCubeCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand MinimizeWindowCommand { get; private set; }
        public ICommand FullscreenWindowCommand { get; private set; }
        public ICommand RandomizeCommand { get; private set; }

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

        //Initialisiert alle ICommands und erstellt einen gelösten Würfel
        public CubeViewModel()
        {
            _rubiksCube = new Cube();

            RotateClockwiseCommand = new RelayCommand(RotateClockwise);
            RotateCounterClockwiseCommand = new RelayCommand(RotateCounterClockwise);
            LoadCubeCommand = new RelayCommand(LoadCube);
            SaveCubeCommand = new RelayCommand(SaveCube);
            CloseWindowCommand = new RelayCommand(CloseWindow);
            MinimizeWindowCommand = new RelayCommand(MinimizeWindow);
            FullscreenWindowCommand = new RelayCommand(FullscreenWindow);
            RandomizeCommand = new RelayCommand(Randomize);

        }

        #endregion

        #region Command Functions

        //Hier befinden sich alle Funktionen die mit den Buttons ausgelöst werden

        private void RotateClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.Clockwise);
        }

        private void RotateCounterClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.CounterClockwise);
        }

        //Lädt einen Würfel aus einer gespeicherten Datei
        private void LoadCube(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Cube Template (*.ctmp)|*.ctmp";
            if (openFileDialog.ShowDialog() == true)
            {
                RubiksCube = LoadSaveCube.LoadCubeFromFile(openFileDialog.FileName);
            }
        }

        //Speichert einen Würfel und erstellt eine Datei dafür
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

        private void Randomize(object Parameter)
        {
            _rubiksCube.Randomize(20);
        }

        #endregion
    }
}
