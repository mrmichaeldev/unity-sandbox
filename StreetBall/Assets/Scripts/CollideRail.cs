using UnityEngine;

public class CollideRail : MonoBehaviour
{
    private PlayerController _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("hit rail");
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
