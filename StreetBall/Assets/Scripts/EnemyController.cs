using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;

    private void Awake()
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(Speed, Speed);
    }

    private void Update()
    {
        var rigidbody2D = GetComponent<Rigidbody2D>();
        Vector2 velocity = rigidbody2D.velocity;
        if (velocity.x <= 0)
            velocity.x = -Speed;
        else velocity.x = Speed;

        if (velocity.y <= 0)
            velocity.y = -Speed;
        else velocity.y = Speed;

        rigidbody2D.velocity = velocity;
    }
}
