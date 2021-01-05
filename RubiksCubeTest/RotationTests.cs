using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksCube.Models;

namespace RubiksCubeTest
{
    [TestClass]
    public class RotationTests
    {
        [TestMethod]
        public void RotateTopC()
        {

            Cube cube = new Cube();
            cube.Rotate(Orientation.Top, Operation.Clockwise);

            bool correct = true;

            for(int i = 0; i < 12; i++)
            {
                if(i < 2)
                {
                    if(cube.Sides[0].Cells[])
                }
            }


        }
    }
}
