using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;

namespace RubiksCube.Models
{

    #region Enums
    public enum Operation
    {
        CounterClockwise = 0,
        Clockwise = 1
    }

    public enum Orientation
    {
        Top,
        Left,
        Front,
        Right,
        Back,
        Down
    }

    #endregion

    public class Side : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string fullpath = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(fullpath));
        }

        #endregion


        #region Public Members

        ObservableCollection<String> _cells;

        public ObservableCollection<String> Cells
        {
            get { return _cells; }
            set
            {
                _cells.Add(value.ToString());
                OnPropertyChanged("Cells");
            }
        }

        #endregion

        #region Constructors

        public Side()
        {
            _cells = new ObservableCollection<String>();
        }

        public Side(string color)
        {
            _cells = new ObservableCollection<String>();
            for (int i = 0; i < 9; i++)
                _cells.Add(color);

        }

        #endregion

    }
}
