using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

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

        string[] Colors = new string[6]
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
                _sides[i] = new Side(Colors[i], (SideColor)i);
            }
        }

        public void SetColor(string currentColor, string newColor)
        {
                currentColor = newColor;
        }

        public void Manipulate(Move move)
        { 

            switch (move)
            {

                //"White"
                //"Yellow"
                //"Blue"
                //"Green"
                //"Red"
                //"Orange"

                case Move.F:

                    break;
            }
        }

    }
}
