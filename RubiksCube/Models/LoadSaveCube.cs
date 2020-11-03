using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace RubiksCube.Models
{
    public class LoadSaveCube
    {
        #region Public Members
        public static void SaveCubeToFile(Cube cube, string filename)
        {
            CubeTemplate cubeTemplate = new CubeTemplate();
            cubeTemplate.CubeColors = cube.Dump().ToList();

            IFormatter formatter = new BinaryFormatter();

            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                formatter.Serialize(stream, cubeTemplate);
        }

        public static Cube LoadCubeFromFile(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                CubeTemplate cubeTemplate;
                try
                {
                    cubeTemplate = (CubeTemplate)formatter.Deserialize(stream);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                    return new Cube();
                }

                Side[] sides = new Side[6];

                for(int i = 0; i < sides.Length; i++)
                {
                    sides[i] = new Side();
                }

                for(int i = 0; i < 6; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        sides[i].Cells.Add(cubeTemplate.CubeColors[(9 * i) + j]);
                    }
                }

                return new Cube(sides);

            }

        }

        #endregion
    }
}
