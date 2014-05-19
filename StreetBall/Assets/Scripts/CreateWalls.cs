﻿using UnityEngine;
using System.Collections;
using System.IO;

public class CreateWalls : MonoBehaviour {

    public GameObject Wall;
    public GameObject VRail;
    public GameObject HRail;
    public GameObject Pickup;
    public GameObject Hole;
    public GameObject Objective;
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

                var position = new Vector3(j, i, 0);
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
                            Objective = (GameObject)Instantiate(Hole, position, Quaternion.identity);
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