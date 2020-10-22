using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RubiksCube
{
    public enum SideColor
    {
        Blue,
        Orange,
        White,
        Red,
        Yellow,
        Green
    }

    class Side
    {
        string[] _cells;

        public string[] Cells
        {
            get { return this._cells; }
        }

        public SideColor SideColor { get; private set; }

        public Side(string color, SideColor sidecolor)
        {
            SideColor = sidecolor;

            _cells = new string[9];
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = color;

        }

    }
}
