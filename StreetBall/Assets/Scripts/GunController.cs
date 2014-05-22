using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject Bullet;

    public Direction Direction;
    public float FireRate;

    private Vector3 _bulletPosition;

    private Quaternion _bulletRotation;
    private float _deltaTime;

    // Use this for initialization
    private void Start()
    {
        switch (Direction)
        {
            case Direction.Left:
            {
                _bulletPosition = new Vector3(transform.position.x - 1, transform.position.y);
                _bulletRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;
            }
            case Direction.Top:
            {
                _bulletPosition = new Vector3(transform.position.x, transform.position.y + 1);
                _bulletRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            }
            case Direction.Right:
            {
                _bulletPosition = new Vector3(transform.position.x + 1, transform.position.y);
                _bulletRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                break;
            }
            case Direction.Bottom:
            {
                _bulletPosition = new Vector3(transform.position.x, transform.position.y - 1);
                _bulletRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;
            }
        }
    }

    // Update is called once per frame
    private void Update()
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