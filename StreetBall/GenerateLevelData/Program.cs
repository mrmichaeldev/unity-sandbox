using Assets.Models;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GenerateLevelData
{
    public class Program
    {
        private const string Path = "@../../../../../Assets/Levels/Json/";

        static void Main(string[] args)
        {
            var serializer = new DataContractSerializer(typeof(GameObjectModels));

            //Directory.Delete(Path, true);

            var stream = File.Create(Path + "Level1.txt");

            var levelData = new GameObjectModels
            {
                Teleporters = new Teleporter[]
                {
                    new Teleporter
                    {
                        Id = 0,
                        Position = new Vector3{X = 1, Y = 2.5f},
                        Rotation = (int)Direction.Left,
                        TargetId = 1,
                    },
                    new Teleporter
                    {
                        Id = 1,
                        Position = new Vector3{ X = 17, Y = 9.5f},
                        Rotation = (int)Direction.Right,
                        TargetId = 0,
                    }
                },
            };

            serializer.WriteObject(stream, levelData);
            stream.Close();

            //GameObjectModels gameObject;
            //using (var memoryStream = new MemoryStream())
            //{
            //    var data = File.ReadAllBytes(string.Format("@../../../../../Assets/Levels/Json/Level{0}.txt", 1));
            //    memoryStream.Write(data, 0, data.Length);
            //    memoryStream.Position = 0;
            //    gameObject = (GameObjectModels)serializer.ReadObject(memoryStream);
            //}
            
        }
    }
}
