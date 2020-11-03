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

    public class Side
    {
        #region Public Members

        string[] _cells;

        public string[] Cells
        {
            get { return this._cells; }
        }

        #endregion

        #region Constructors

        public Side()
        {
            _cells = new string[9];
        }

        public Side(string color)
        {
            _cells = new string[9];
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = color;

        }

        #endregion

    }
}
