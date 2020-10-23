using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RubiksCube
{
    public enum Orientation
    {
        Top,
        Left,
        Front,
        Right,
        Back,
        Down
    }

    class Side
    {
        string[] _cells;

        public string[] Cells
        {
            get { return this._cells; }
        }

        public Orientation Orientation { get; private set; }

        public Side(string color, Orientation orientation)
        {
            this.Orientation = orientation;

            _cells = new string[9];
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = color;

        }

    }
}
