using System;
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

        //Event für INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string fullpath = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fullpath));
        }

        #endregion

        #region Private Members

        //Objekt des Würfels
        private Cube _rubiksCube;

        #endregion

        #region Public Members

        //ICommand Objekte für die Buttons
        public ICommand RotateClockwiseCommand { get; private set; }
        public ICommand RotateCounterClockwiseCommand { get; private set; }
        public ICommand SaveCubeCommand { get; private set; }
        public ICommand LoadCubeCommand { get; private set; }
        public ICommand RandomizeCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        //Eigenschaftsmethode für den Cube
        public Cube RubiksCube
        {
            get { return _rubiksCube; }
            set { 
                if (_rubiksCube == value) 
                    return;
                _rubiksCube = value;
                //UI aktualisieren
                OnPropertyChanged("RubiksCube");
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
            RandomizeCommand = new RelayCommand(Randomize);
            ResetCommand = new RelayCommand(Reset);

        }

        #endregion

        #region Command Functions

        //Hier befinden sich alle Funktionen die mit den Buttons ausgelöst werden

        //Seite des Würfel im Uhrzeigersinn drehen
        private void RotateClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.Clockwise);
        }

        //Seite des Würfels gegen den Uhrzeigersinn drehen
        private void RotateCounterClockwise(object parameter)
        {
            RubiksCube.Rotate((Orientation)parameter, Operation.CounterClockwise);
        }

        //Lädt einen Würfel aus einer gespeicherten Datei
        private void LoadCube(object parameter)
        {
            //FileDialog öffnen und nur .ctmp Datein zulassen
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
            //FileDialog öffnen und nur .ctmp Datein zulassen
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Cube Template (*.ctmp)|*.ctmp";

            if (saveFileDialog.ShowDialog() == true)
            {
                LoadSaveCube.SaveCubeToFile(RubiksCube, saveFileDialog.FileName);
            }
        }

        //Dreht die Seiten 20 mal zufällig
        private void Randomize(object Parameter)
        {
            RubiksCube.Randomize(20);
        }

        //Erstellt einen neuen gelösten Würfel
        private void Reset(object parameter)
        {
            RubiksCube = new Cube();
        }

        #endregion
    }
}
