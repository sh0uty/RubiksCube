using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace RubiksCube
{
    class Cube : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        Side[] _sides;

        public Side[] Sides
        {
            get { return _sides; }
        }

        private int[,] changeIndexClockwise = new int[,] {
            { 0, 6 }, { 3, 7 }, { 6, 8 }, { 7, 5 }, { 8, 2 }, { 5, 1 } 
        };

        private string[] Colors = new string[6]
        {
            "Blue",
            "Orange",
            "White",
            "Red",
            "Yellow",
            "Green"
        };

        private int[,] Indexes;

        public Cube()
        {
            _sides = new Side[6];

            for(int i = 0; i < _sides.Length; i++)
            {
                _sides[i] = new Side(Colors[i], (Orientation)i);
            }
        }

        public void SetColor(string currentColor, string newColor)
        {
                currentColor = newColor;
        }

        public void Rotate(Orientation orientation, Operation operation)
        { 

            switch (orientation)
            {

                case Orientation.Front:
                    Manipulate(new int[] { 6, 7, 8, 27, 30, 33, 47, 46, 45, 17, 14, 11 }, orientation, operation);
                    break;
                case Orientation.Right:
                    Manipulate(new int[] { 8, 5, 2, 36, 39, 42, 53, 50, 47, 26, 23, 20 }, orientation, operation);
                    break;
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

        private void RotateOrientationSide(Orientation orientation)
        {
            string[] tempColor = new string[] { GetColorFromCells((int)orientation * 9 + 0), GetColorFromCells((int)orientation * 9 + 3) };

            for(int i = 0; i < 6; i++)
            {
                SetColorFromCell((int)orientation * 9 + changeIndexClockwise[ i, 0 ], GetColorFromCells((int)orientation * 9 + changeIndexClockwise[ i, 1 ]));
            }
            SetColorFromCell((int)orientation * 9 + 2, tempColor[0]);
            SetColorFromCell((int)orientation * 9 + 1, tempColor[1]);
        }

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

    }
}
