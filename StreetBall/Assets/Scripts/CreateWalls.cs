using UnityEngine;
using System.Collections;
using System.IO;

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
	void Start () {

        var lines = File.ReadAllLines(string.Format(@"Assets/Levels/Level{0}.txt", Level), System.Text.Encoding.UTF8);
        for (int i = 0; i < lines.Length; i++ )
        {
            var codes = lines[i].Split(' ');
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
                            Hole = (GameObject)Instantiate(Hole, position, Quaternion.identity);
                            break;
                        }
                    case 5:
                        {
                            Gun.GetComponent<GunController>().Direction = Enumeration.Direction.Left;
                            Instantiate(Gun, position, Quaternion.Euler(new Vector3(0, 0, 0)));
                            break;
                        }
                    case 6:
                        {
                            Gun.GetComponent<GunController>().Direction = Enumeration.Direction.Top;
                            Instantiate(Gun, position, Quaternion.Euler(new Vector3(0, 0, -90)));
                            break;
                        }
                    case 7:
                        {
                            Gun.GetComponent<GunController>().Direction = Enumeration.Direction.Right;
                            Instantiate(Gun, position, Quaternion.Euler(new Vector3(0, 0, 180)));
                            break;
                        }
                    case 8:
                        {
                            Gun.GetComponent<GunController>().Direction = Enumeration.Direction.Bottom;
                            Instantiate(Gun, position, Quaternion.Euler(0, 0, 90));
                            break;
                        }
                    case 9:
                        {
                            Instantiate(Plasma, position, Quaternion.identity);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        var pickupLines = File.ReadAllLines(string.Format(@"Assets/Pickups/Level{0}.txt", Level), System.Text.Encoding.UTF8);
        for (int i = 0; i < pickupLines.Length; i++ )
        {
            var codes = pickupLines[i].Split(' ');
            for (int j = 0; j < codes.Length; j++)
            {
                int num;
                int.TryParse(codes[j], out num);

                var position = new Vector3(j, i, 0);
                switch (num)
                {
                    case 1:
                        {
                            Instantiate(Pickup, position, Quaternion.identity);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
