using Assets.Models;
using Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CreateWalls : MonoBehaviour
{
    public GameObject Wall;
    public GameObject VRail;
    public GameObject HRail;
    public GameObject Pickup;
    public GameObject Hole;
    public GameObject Gun;
    public GameObject Plasma;
    public GameObject Teleportor;
    public int Level;

    // Use this for initialization
    private void Start()
    {
        string[] lines = File.ReadAllLines(string.Format(@"Assets/Levels/Maps/Level{0}.txt", Level), Encoding.UTF8);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] codes = lines[i].Split(' ');
            for (int j = 0; j < codes.Length; j++)
            {
                int num;
                int.TryParse(codes[j], out num);

                var position = new Vector3(j, lines.Length - i, 0);
                switch (num)
                {
                    case 1:
                    {
                        Instantiate(Wall, position, Quaternion.identity);
                        break;
                    }
                    case 2:
                    {
                        Instantiate(VRail, position, Quaternion.identity);
                        break;
                    }
                    case 3:
                    {
                        Instantiate(HRail, position, Quaternion.identity);
                        break;
                    }
                    case 4:
                    {
                        Hole = (GameObject) Instantiate(Hole, position, Quaternion.identity);
                        break;
                    }
                    case 5:
                    {
                        Gun.GetComponent<GunController>().Direction = Direction.Left;
                        Instantiate(Gun, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                        break;
                    }
                    case 6:
                    {
                        Gun.GetComponent<GunController>().Direction = Direction.Top;
                        Instantiate(Gun, position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        break;
                    }
                    case 7:
                    {
                        Gun.GetComponent<GunController>().Direction = Direction.Right;
                        Instantiate(Gun, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                        break;
                    }
                    case 8:
                    {
                        Gun.GetComponent<GunController>().Direction = Direction.Bottom;
                        Instantiate(Gun, position, Quaternion.Euler(0, 0, 90));
                        break;
                    }
                    case 9:
                    {
                        Instantiate(Plasma, position, Quaternion.identity);
                        break;
                    }
                }
            }
        }

        string[] pickupLines = File.ReadAllLines(string.Format(@"Assets/Levels/Pickups/Level{0}.txt", Level), Encoding.UTF8);
        for (int i = 0; i < pickupLines.Length; i++)
        {
            string[] codes = pickupLines[i].Split(' ');
            for (int j = 0; j < codes.Length; j++)
            {
                int num;
                int.TryParse(codes[j], out num);

                var position = new Vector3(j, pickupLines.Length - i, 0);
                switch (num)
                {
                    case 1:
                    {
                        Instantiate(Pickup, position, Quaternion.identity);
                        break;
                    }
                }
            }
        }

        //var gameObject = JsonConvert.DeserializeObject<GameObjectModels>(levelJson);
        //var serializer = new DataContractSerializer(typeof(GameObjectModels));
        var serializer = new DataContractSerializer(typeof(GameObjectModels));
        //Debug.Log(File.ReadAllLines(string.Format(@"Assets/Levels/Json/Level{0}.txt", Level)).First());
        //var gameObject = (GameObjectModels)serializer.ReadObject(System.Xml.XmlReader.Create(string.Format(@"Assets/Levels/Json/Level{0}.txt", Level)));
        //var stream = File.OpenRead(string.Format(@"Assets/Levels/Json/Level{0}.txt", Level));
        //stream.Write(
        //var gameObject = (GameObjectModels)serializer.ReadObject(stream);
        //Debug.Log(gameObject.Teleporters.Last().Position.x);

        GameObjectModels gameObject;

        using (var stream = new MemoryStream())
        {
            var data = File.ReadAllBytes(string.Format(@"Assets/Levels/Json/Level{0}.txt", Level));
            Debug.Log(System.Text.Encoding.UTF8.GetString(data, 0, data.Length));
            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            gameObject = (GameObjectModels)serializer.ReadObject(stream);
        }

        if (gameObject.Buttons != null)
        {
            foreach (var buttons in gameObject.Buttons)
            {
                //TODO: instantiate a button
            }
        }

        if (gameObject.Teleporters != null)
        {
            foreach (var teleporter in gameObject.Teleporters)
            {
                Instantiate(Teleportor, teleporter.Position.GetVector(), Quaternion.Euler(new Vector3(0, 0, teleporter.Rotation * 90 - 90)));
            }
        }

        if (gameObject.Traps != null)
        {
            foreach (var trap in gameObject.Traps)
            {
                //Instantiate traps

            }
        }
    }
}
