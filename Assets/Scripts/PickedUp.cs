using UnityEngine;  
using System.Collections;

public class PickedUp : MonoBehaviour
{
    private PlayerController _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            _player.NumPickups++;
        }
    }
}
