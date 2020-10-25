using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace RubiksCube
{
    class Cube : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion

        #region Public Members

        Side[] _sides;

        public Side[] Sides
        {
            get { return _sides; }
        }

        #endregion

        #region Privte Members

        private int[] SideRotationIndexes = new int[] { 0, 1, 2, 5, 8, 7, 6, 3 };

        private string[] Colors = new string[6]
        {
            "Blue",
            "Orange",
            "White",
            "Red",
            "Yellow",
            "Green"
        };

        #endregion

        #region Constructors
        public Cube()
        {
            _sides = new Side[6];

            for(int i = 0; i < _sides.Length; i++)
            {
                _sides[i] = new Side(Colors[i], (Orientation)i);
            }
        }

        #endregion

        #region Public Methods

        public void Rotate(Orientation orientation, Operation operation)
        { 
            switch (orientation)
            {
                case Orientation.Top:
                    Manipulate(new int[] { 38, 37, 36, 29, 28, 27, 20, 19, 18, 11, 10, 9 }, orientation, operation);
                    break;
                case Orientation.Left:
                    Manipulate(new int[] { 18, 21, 24, 45, 48, 51, 38, 41, 44, 0, 3, 6}, orientation, operation);
                    break;
                case Orientation.Front:
                    Manipulate(new int[] { 6, 7, 8, 27, 30, 33, 47, 46, 45, 17, 14, 11 }, orientation, operation);
                    break;
                case Orientation.Right:
                    Manipulate(new int[] { 8, 5, 2, 36, 39, 42, 53, 50, 47, 26, 23, 20 }, orientation, operation);
                    break;
                case Orientation.Back:
                    Manipulate(new int[] { 0 }, orientation, operation);
                    break;
                case Orientation.Down:
                    Manipulate(new int[] { 0 }, orientation, operation);
                    break;
                default: break;
            }
        }

        #endregion

        #region Private Methods

        private string GetColorFromCells(int cell)
        {
            return Sides[cell / 9].Cells[cell % 9];
        }

        private void SetColorFromCell(int cell, string color)
        {
            Sides[cell / 9].Cells[cell % 9] = color;
        }

        private void RotateOrientationSide(Orientation orientation, Operation operation)
        {
            List<int> Positions = new List<int>();
            Positions.AddRange(SideRotationIndexes);

            Queue<string> Colors = new Queue<string>();
            Positions.ForEach(position => Colors.Enqueue(GetColorFromCells((int)orientation * 9 + position)));

            if(operation == Operation.Clockwise)
            {
                for (int i = 0; i < 6; i++)
                    Colors.Enqueue(Colors.Dequeue());
            }

            if(operation == Operation.CounterClockwise)
            {
                for (int i = 0; i < 2; i++)
                    Colors.Enqueue(Colors.Dequeue());
            }

            Positions.ForEach(pos => SetColorFromCell((int)orientation * 9 + pos, Colors.Dequeue()));
        }

        private void Manipulate(int[] cells, Orientation orientation, Operation operation)
        {
            Queue<string> Colors = new Queue<string>();

            cells.ToList().ForEach(cell => Colors.Enqueue(GetColorFromCells(cell)));

            if (operation == Operation.Clockwise)
            {
                for(int i = 0; i < 9; i++)
                    Colors.Enqueue(Colors.Dequeue());
            }

            if(operation == Operation.CounterClockwise)
            {

                for (int i = 0; i < 3; i++)
                    Colors.Enqueue(Colors.Dequeue());
            }

            cells.ToList().ForEach(cell => SetColorFromCell(cell, Colors.Dequeue()));

            RotateOrientationSide(orientation, operation);

        }
        #endregion
    }
}
