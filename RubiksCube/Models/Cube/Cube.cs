using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RubiksCube.Models
{
    public class Cube
    {
        #region Public Members

        Side[] _sides;

        public Side[] Sides
        {
            get { return _sides; }
        }

        #endregion

        #region Privte Members

        private int[] SideRotationIndexes = new int[] { 0, 1, 2, 5, 8, 7, 6, 3 };

        private Random random;

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
            random = new Random();

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
                    Manipulate(new int[] { 18, 21, 24, 45, 48, 51, 44, 41, 38, 0, 3, 6}, orientation, operation);
                    break;
                case Orientation.Front:
                    Manipulate(new int[] { 6, 7, 8, 27, 30, 33, 47, 46, 45, 17, 14, 11 }, orientation, operation);
                    break;
                case Orientation.Right:
                    Manipulate(new int[] { 8, 5, 2, 36, 39, 42, 53, 50, 47, 26, 23, 20 }, orientation, operation);
                    break;
                case Orientation.Back:
                    Manipulate(new int[] { 2, 1, 0, 9, 12, 15, 51, 52, 53, 35, 32, 29 }, orientation, operation);
                    break;
                case Orientation.Down:
                    Manipulate(new int[] { 15, 16, 17, 24, 25, 26, 33, 34, 35, 42, 43, 44 }, orientation, operation);
                    break;
                default: break;
            }
        }

        public void Randomize(int moves)
        {
            Orientation orientation;
            Operation operation;

            for(int i = 0; i < moves; i++)
            {
                orientation = (Orientation)random.Next(6);
                operation = (Operation)random.Next(2);

                Debug.WriteLine(String.Format("{0} : {1}", orientation, operation));

                Rotate(orientation, operation);
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
