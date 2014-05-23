using Assets.Models;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
                        Position = new Vector2(0,0),
                        Direction = Direction.Right,
                        TargetId = 1,
                    },
                    new Teleporter
                    {
                        Id = 1,
                        Position = new Vector2(19,12),
                        Direction = Direction.Left,
                        TargetId = 0,
                    }
                },
            };

            serializer.WriteObject(stream, levelData);
        }
    }
}
