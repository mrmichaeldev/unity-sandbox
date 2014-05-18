using UnityEngine;
using System.Collections;

public class CollideRail : MonoBehaviour 
{

    private PlayerController _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!(_player.IsOnHRail || _player.IsOnVRail))
        {
            switch (collider.tag)
            {
                case "VRail":
                    {
                        _player.IsOnVRail = true;
                        _player.IsOnHRail = false;
                        break;
                    }
                case "HRail":
                    {
                        _player.IsOnHRail = true;
                        _player.IsOnVRail = false;
                        break;
                    }
            }
        }
    }
}
