using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

    public class Cube
    {
        #region Public Members

        public Side[] Sides
        {
            get { return _sides; }
        }

        /* Matrix zum rotieren der Seiten anhand der Position der Farben
        
        #  #  #  | 0  1  2  |  #  #  #   #  #  #
        #  #  #  | 3  4  5  |  #  #  #   #  #  #
        #  #  #  | 6  7  8  |  #  #  #   #  #  #
        -----------------------------------------
        9  10 11 | 18 19 20 | 27 28 29 | 36 37 38 
        12 13 14 | 21 22 23 | 30 31 32 | 29 40 41
        15 16 17 | 24 25 26 | 33 34 35 | 42 43 44
        -----------------------------------------
        #  #  #  | 45 46 47 |  #  #  #    #  #  #
        #  #  #  | 48 49 50 |  #  #  #    #  #  #
        #  #  #  | 51 52 53 |  #  #  #    #  #  #
         */
        public int[,] RotationMatrix = new int[,]
        {
            { 38, 37, 36, 29, 28, 27, 20, 19, 18, 11, 10, 9 },
            { 18, 21, 24, 45, 48, 51, 44, 41, 38, 0, 3, 6},
            { 6, 7, 8, 27, 30, 33, 47, 46, 45, 17, 14, 11 },
            { 8, 5, 2, 36, 39, 42, 53, 50, 47, 26, 23, 20 },
            { 2, 1, 0, 9, 12, 15, 51, 52, 53, 35, 32, 29 },
            { 15, 16, 17, 24, 25, 26, 33, 34, 35, 42, 43, 44 }
        };

        #endregion

        #region Privte Members

        private Side[] _sides;

        //Reihenfolge einer Seite die eine Rotation als Kreis ermöglicht
        private int[] SideRotationIndexes = new int[] { 0, 1, 2, 5, 8, 7, 6, 3 };

        private Random random;

        //Seitenfarben des Würfels
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
        //Neuen Würfel erstellen mit vordefinierten Farben (Würfel ist gelöst)
        public Cube()
        {
            _sides = new Side[6];
            random = new Random();

            for(int i = 0; i < _sides.Length; i++)
            {
                _sides[i] = new Side(Colors[i]);
            }
        }

        //Würfel erstellen mit vordefinierten Seiten
        public Cube(Side[] sides)
        {
            _sides = sides.ToArray();
            random = new Random();
        }

        #endregion

        #region Public Methods

        //Seite drehen mithilfe der Seite und Richtung
        public void Rotate(Orientation orientation, Operation operation)
        { 
            switch (orientation)
            {
                case Orientation.Top:
                    Manipulate(GetRow(RotationMatrix, 0), orientation, operation);
                    break;
                case Orientation.Left:
                    Manipulate(GetRow(RotationMatrix, 1), orientation, operation);
                    break;
                case Orientation.Front:
                    Manipulate(GetRow(RotationMatrix, 2), orientation, operation);
                    break;
                case Orientation.Right:
                    Manipulate(GetRow(RotationMatrix, 3), orientation, operation);
                    break;
                case Orientation.Back:
                    Manipulate(GetRow(RotationMatrix, 4), orientation, operation);
                    break;
                case Orientation.Down:
                    Manipulate(GetRow(RotationMatrix, 5), orientation, operation);
                    break;
                default: break;
            }
        }

        //Zufällige Seitenrotationen
        public void Randomize(int moves)
        {
            Orientation orientation, preOr = 0;
            Operation operation, preOp = 0;

            for (int i = 0; i < moves; i++)
            {
                orientation = (Orientation)random.Next(6);
                operation = (Operation)random.Next(2);

                if (i != 0)
                {
                    if (orientation == preOr)
                    {
                        if (operation != preOp)
                        {
                            i--;
                            continue;
                        }
                    }
                }

                Rotate(orientation, operation);

                preOr = orientation;
                preOp = operation;
            }
        }

        //Gibt alle farben in Reihenfolge zurück und macht ein IEnumerable raus
        public IEnumerable<String> Dump()
        {
            foreach(Side s in Sides)
            {
                foreach(String col in s.Cells)
                {
                    yield return col;
                }
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

        //Funktion die die 9 Farben einer Seite im kreis dreht
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

        //Manipuliert die Reihenfolge der Strings in einem übergebenen Array
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

        //Gibst die n-te Reihe eines 2-dimensionalen Arrays als Array wieder
        public int[] GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        #endregion
    }
}
