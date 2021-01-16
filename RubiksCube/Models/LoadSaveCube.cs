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
        #region Public Static Methods

        //Speichern eines Würfels eine Datei mithilfe der Funktion Serialize eines IFormatter
        public static void SaveCubeToFile(Cube cube, string filename)
        {
            //CubeTemplate erstellen und dessen Liste mit den aktuellen Farben des Würfels befüllen
            CubeTemplate cubeTemplate = new CubeTemplate()
            {
                CubeColors = cube.Dump().ToList()
            };
            
            //IFormatter erstellen zum speichern der Liste
            IFormatter formatter = new BinaryFormatter();

            //Stream erstellen und damit die Liste in eine Datei speichern
            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                formatter.Serialize(stream, cubeTemplate);
        }

        //Laden eines Würfels aus einer Datei mithilfe der Funktion Deserialize eines IFormatter
        public static Cube LoadCubeFromFile(string filename)
        {
            //IFormatter erstellen zum laden der Liste aus einer Datei
            IFormatter formatter = new BinaryFormatter();

            //Stream zu der angegebenen Datei öffnen
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                CubeTemplate cubeTemplate;

                //Versuchen die Liste aus der Datei zu laden und Fehlermeldungen abfangen falls die Datei ungültig iist
                try
                {
                    cubeTemplate = (CubeTemplate)formatter.Deserialize(stream);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error. Not a valid file!", MessageBoxButton.OK);
                    return new Cube();
                }

                //Side-Array erstellen
                Side[] sides = new Side[6];

                //Jedes Element des Arrays initialisieren
                for(int i = 0; i < sides.Length; i++)
                {
                    sides[i] = new Side();
                }

                //Den Array mit den Farben füllen
                for(int i = 0; i < 6; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        sides[i].Cells.Add(cubeTemplate.CubeColors[(9 * i) + j]);
                    }
                }

                //Neuen Würfel zurückgeben mit den vordefinierten Seiten
                return new Cube(sides);

            }

        }

        #endregion
    }
}
