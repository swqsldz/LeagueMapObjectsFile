using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LeagueMapObjectsFile
{
    class Program
    {
        static void Main(string[] args)
        {
            MapObjectsFile newMob = new MapObjectsFile(@"C:\Wooxy\extract\lol_game_client\LEVELS\map8\scene\MapObjects.mob");
            using (StreamWriter sw = new StreamWriter(File.Open(@"C:\Wooxy\extract\lol_game_client\LEVELS\map8\scene\objects8.txt", FileMode.Create)))
            {
                foreach (MapObjectsFile.MapObjectsFileObject mapObject in newMob.Objects)
                {
              
                    sw.WriteLine("Type = " + String.Format("{0:00}", (byte)mapObject.Type) + " // " + mapObject.Name + " // " + mapObject.Position[0] + " ; " + mapObject.Position[1] + " ; " + mapObject.Position[2]);
                }
            }

        }
    }
}
