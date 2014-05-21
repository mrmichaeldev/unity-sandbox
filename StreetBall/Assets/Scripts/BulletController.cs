using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Enumeration.Direction Direction;
    public bool ShouldDestroy;
    public int Speed;

    // Use this for initialization
    private void Start()
    {
        switch (Direction)
        {
            case Enumeration.Direction.Left:
            {
                rigidbody2D.velocity = new Vector2(-1*Speed, 0);
                break;
            }
            case Enumeration.Direction.Top:
            {
                rigidbody2D.velocity = new Vector2(0, 1*Speed);
                break;
            }
            case Enumeration.Direction.Right:
            {
                rigidbody2D.velocity = new Vector2(1*Speed, 0);
                break;
            }
            case Enumeration.Direction.Bottom:
            {
                rigidbody2D.velocity = new Vector2(0, -1*Speed);
                break;
            }
            default:
                break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: If out of bounds, need to get reference to camera?
        if (ShouldDestroy || transform.position.x < -10 || transform.position.x > 29 || transform.position.y < -10 ||
            transform.position.y > 22)
        {
            Destroy(gameObject);
        }
    }
}