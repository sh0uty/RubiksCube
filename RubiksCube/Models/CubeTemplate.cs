using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiksCube.Models
{
    public class CubeTemplate
    {
        List<String> CubeColors;


        public CubeTemplate()
        {
            CubeColors = new List<string>();
        }

        public CubeTemplate(Cube cube)
        {
            CubeColors = cube.Dump().ToList();
        }
    }
}
