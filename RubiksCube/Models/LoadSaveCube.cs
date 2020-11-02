using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RubiksCube.Models
{
    public class LoadSaveCube
    {
        public static void SaveCubeToFile(Cube cube, string filename)
        {
            CubeTemplate cubeTemplate = new CubeTemplate();
            cubeTemplate.CubeColors = cube.Dump().ToList();

            IFormatter formatter = new BinaryFormatter();

            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                formatter.Serialize(stream, cubeTemplate);
        }

        public static void LoadCubeFromFile(string filename)
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                CubeTemplate cubeTemplate = (CubeTemplate)formatter.Deserialize(stream);
            }
        }
    }
}
