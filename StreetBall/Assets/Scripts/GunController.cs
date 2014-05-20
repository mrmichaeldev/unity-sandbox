using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

    public GameObject Bullet;

    public float FireRate;

    private float _deltaTime;

    public Enumeration.Direction Direction;

    private Vector3 _bulletPosition;

    private Quaternion _bulletRotation;

	// Use this for initialization
	void Start ()
    {
        switch (Direction)
        {
            case Enumeration.Direction.Left:
                {
                    _bulletPosition = new Vector3(transform.position.x - 1, transform.position.y);
                    _bulletRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    break;
                }
            case Enumeration.Direction.Top:
                {
                    _bulletPosition = new Vector3(transform.position.x, transform.position.y + 1);
                    _bulletRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;
                }
            case Enumeration.Direction.Right:
                {
                    _bulletPosition = new Vector3(transform.position.x + 1, transform.position.y);
                    _bulletRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    break;
                }
            case Enumeration.Direction.Bottom:
                {
                    _bulletPosition = new Vector3(transform.position.x, transform.position.y - 1);
                    _bulletRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    break;
                }

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        _deltaTime += Time.deltaTime;

        if (_deltaTime >= FireRate)
        {
            Bullet.GetComponent<BulletController>().Direction = Direction;

            Instantiate(Bullet, _bulletPosition, _bulletRotation);
            _deltaTime = 0;
        }
    }
}
