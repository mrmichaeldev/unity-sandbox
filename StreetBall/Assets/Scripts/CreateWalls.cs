using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class CreateWalls : MonoBehaviour
{
    public GameObject Wall;
    public GameObject VRail;
    public GameObject HRail;
    public GameObject Pickup;
    public GameObject Hole;
    public GameObject Gun;
    public GameObject Plasma;
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

        string[] jsonLines = File.ReadAllLines(string.Format(@"Assets/Levels/Json/Level{0}.txt", Level), Encoding.UTF8);
        for (int i = 0; i < jsonLines.Length; i++)
        {
            var serializedGameObject = jsonLines[i];
            //TODO need to pull type for this?
            var gameObject = JsonConvert.DeserializeObject(serializedGameObject);

        }

    }
}
