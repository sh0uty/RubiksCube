using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RubiksCube
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

    class Side
    {
        #region Public Members

        string[] _cells;

        public string[] Cells
        {
            get { return this._cells; }
        }

        public Orientation Orientation { get; private set; }

        #endregion

        #region Constructors
        public Side(string color, Orientation orientation)
        {
            this.Orientation = orientation;

            _cells = new string[9];
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = color;

        }

        #endregion

    }
}
