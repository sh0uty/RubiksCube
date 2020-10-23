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
    public enum Operation
    {
        CounterClockwise = -1,
        Clockwise = 1
    }

    class Cube : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        Side[] _sides;

        public Side[] Sides
        {
            get { return _sides; }
        }

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

        public Cube()
        {
            _sides = new Side[6];

            for(int i = 0; i < _sides.Length; i++)
            {
                _sides[i] = new Side(Colors[i], (Orientation)i);
            }
        }

        public void Rotate(Orientation orientation, Operation operation)
        { 

            switch (orientation)
            {
                case Orientation.Top:
                    Manipulate(new int[] { 0 }, orientation, operation);
                    break;
                case Orientation.Left:
                    Manipulate(new int[] { 0 }, orientation, operation);
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
            /*
            string[] tempColor = new string[] { GetColorFromCells((int)orientation * 9 + 0), GetColorFromCells((int)orientation * 9 + 3) };

            for(int i = 0; i < 6; i++)
            {
                SetColorFromCell((int)orientation * 9 + changeIndexClockwise[ i, 0 ], GetColorFromCells((int)orientation * 9 + changeIndexClockwise[ i, 1 ]));
            }
            SetColorFromCell((int)orientation * 9 + 2, tempColor[0]);
            SetColorFromCell((int)orientation * 9 + 1, tempColor[1]);
            */

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
        /*
        private void Manipulate(int[] cells, Orientation orientation, Operation operation)
        {

            if(operation == Operation.Clockwise)
            {
                string[] ReplacementRow = new string[3];
                string[] RowToBeReplaced = new string[3];
                for (int i = 0; i < 4; i++)
                {

                    if(i != 0)
                    {
                        ReplacementRow = RowToBeReplaced.ToArray();
                    }

                    for (int j = 0; j < 3; j++)
                    {
                        RowToBeReplaced[j] = GetColorFromCells(cells[(i * 3 + j + 3) % 12]);

                        if (i == 0)
                        {
                            SetColorFromCell(cells[(i * 3 + j + 3) % 12], GetColorFromCells(cells[(i * 3 + j) % 12]));
                        }
                        else
                        {
                            SetColorFromCell(cells[(i * 3 + j + 3) % 12], ReplacementRow[j]);
                        }

                    }
                }
                RotateOrientationSide(orientation);
            }
            else
            {

            }
        }
        */

    }
}
