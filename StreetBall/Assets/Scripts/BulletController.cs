using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public int Speed;

    public bool ShouldDestroy;

	// Use this for initialization
	void Start ()
    {
        rigidbody2D.velocity = new Vector2(-1 * Speed, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: If out of bounds, need to get reference to camera?
        if (ShouldDestroy || transform.position.x < -10 || transform.position.x > 29 || transform.position.y < -10 || transform.position.y > 22)
        {
            Destroy(gameObject);
        }
	}
}
