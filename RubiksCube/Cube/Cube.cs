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

        public void Manipulate(Orientation orientation, Operation operation)
        { 

            switch (orientation)
            {

                case Orientation.Front:

                    Move(new int[] { 6, 7, 8, 27, 30, 33, 47, 46, 45, 17, 14, 11 }, operation);

                    break;

            }
        }

        private void Move(int[] cells, Operation operation)
        {

            if(operation == Operation.Clockwise)
            {
                string ReplacementColor= "";
                string ColorToBeReplaced = "";
                for (int i = 0; i < 4; i++)
                {
                    ReplacementColor = ColorToBeReplaced;
                    ColorToBeReplaced = Sides[cells[ (i * 3 + 3) %  12] / 9].Cells[cells[(i * 3 + 3) % 12 ] % 9];

                    for (int j = 0; j < 3; j++)
                    { 
                        if(i == 0)
                        {
                            Sides[cells[(i * 3 + j + 3) % 12] / 9].Cells[cells[(i * 3 + j + 3) % 12] % 9] = Sides[cells[(i * 3 + j) % 12] / 9].Cells[cells[(i * 3 + j) % 12] % 9];
                        }
                        else
                        {
                            Sides[cells[(i * 3 + j + 3) % 12] / 9].Cells[cells[(i * 3 + j + 3) % 12] % 9] = ReplacementColor;
                        }
                    }

                }
            }
            else
            {

            }
        }

    }
}
