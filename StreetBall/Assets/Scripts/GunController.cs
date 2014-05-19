using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    public GameObject Bullet;

    private const float _fireRate = 5.0f;

    private float _deltaTime;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        _deltaTime += Time.deltaTime;

        if (_deltaTime >= _fireRate)
        {
            Instantiate(Bullet, new Vector3(transform.position.x - 1, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 90)));
            _deltaTime = 0;
        }
    }
}
