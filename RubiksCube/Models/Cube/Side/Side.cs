using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace RubiksCube.Models
{

    public class Side : INotifyPropertyChanged
    {

        #region Events

        //ProperyChanged Event für die UI-Aktualisierung wenn sich eine Farbe am Würfel ändert
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string fullpath = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fullpath));
        }

        #endregion

        #region Public Members
        //Liste der 9 Farben pro Seite als OberservableCollection da die Klasse auch INotifyPropertyChanged benutzt
        private ObservableCollection<String> _cells;

        //Eigenschaftsmethode für _cells
        public ObservableCollection<String> Cells
        {
            get { return _cells; }
            set
            {
                _cells.Add(value.ToString());
                
                //Cells für UI Aktualisieren
                OnPropertyChanged("Cells");
            }
        }

        #endregion

        #region Constructors

        //Konstruktor für eine Leere Seite
        public Side()
        {
            _cells = new ObservableCollection<String>();
        }

        //Konstruktor für eine Seite mit bestimmten Farben
        public Side(string color)
        {
            _cells = new ObservableCollection<String>();
            for (int i = 0; i < 9; i++)
                _cells.Add(color);

        }

        #endregion

    }
}
