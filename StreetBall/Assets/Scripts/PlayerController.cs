using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;

    public bool OnRail { get { return IsOnHRail || IsOnVRail; } }

    public bool IsOnHRail;
    public bool IsOnVRail;
    public int NumPickups;
    public bool IsInHole;

    public bool IsLevelComplete { get { return NumPickups == 3; } }

    public float? DeltaAnimationTime;

    public int Health = 5;

    //Fields for controlling player when entering objective
    private GameObject _hole;
    private Vector2? _preAnimationVelocity;
    private Vector2? _animationVelocity;
    private const float _animationDelay = 0.55f;
    private CircleCollider2D _collider;

    private void Start()
    {
        Application.targetFrameRate = 60;
        rigidbody2D.velocity = new Vector2(Speed, Speed);
        _collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: figure out how to do this in Awake()
        if (_hole == null)
        {
            _hole = GameObject.FindWithTag("Hole");
        }

        if (!IsInHole)
        {
            CollideRail();
            NormalizeVelocity();
        }
        else
        {
            if (DeltaAnimationTime != null)
            {
                DeltaAnimationTime = DeltaAnimationTime + Time.deltaTime;
                if (!IsLevelComplete)
                {
                    if (DeltaAnimationTime >= _animationDelay)
                    {
                        _animationVelocity = rigidbody2D.velocity = _animationVelocity.Value*-1f;
                        DeltaAnimationTime = null;
                    }
                }
                else
                {
                    if (DeltaAnimationTime >= _animationDelay)
                    {
                        rigidbody2D.velocity = Vector2.zero;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Enemy":
                {
                    IsOnVRail = IsOnHRail = false;
                    break;
                }
            case "PlasmaEnemy":
                {
                    TakeLethalDamage();
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Hole":
            {
                if (collider.tag == "Hole")
                {
                    IsInHole = true;
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"),
                        true);
                    if (IsLevelComplete)
                    {
                        if (DeltaAnimationTime == null && _animationVelocity == null)
                        {
                            DeltaAnimationTime = 0;
                            _animationVelocity =
                                rigidbody2D.velocity =
                                    new Vector2(_hole.transform.position.x - transform.position.x,
                                        _hole.transform.position.y - transform.position.y)/0.6f;
                            GetComponent<Animator>().SetTrigger("LevelComplete");
                        }
                    }
                    else
                    {
                        if (DeltaAnimationTime == null && _animationVelocity == null)
                        {
                            DeltaAnimationTime = 0;
                            _preAnimationVelocity = rigidbody2D.velocity;
                            _animationVelocity =
                                rigidbody2D.velocity =
                                    new Vector2(_hole.transform.position.x - transform.position.x,
                                        _hole.transform.position.y - transform.position.y)/0.6f;
                            GetComponent<Animator>().SetTrigger("LevelNotComplete");
                        }
                    }
                }
                break;
            }
            case "Teleporter":
            {
                var component = collider.GetComponent<TeleporterController>();
                //var p0 = component.Teleporter.Position;
                //var p1 = component.TeleportTo.GetComponent<TeleporterController>().Teleporter.Position;
                transform.Translate(component.Teleport);
                Debug.Log(component.Teleport);
                break;
            }
            case "Bullet":
            {
                TakeDamage();
                collider.GetComponent<BulletController>().ShouldDestroy = true;
                break;
            }
            case "Plasma":
            {
                TakeLethalDamage();
                break;
            }
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Hole":
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
                transform.Translate(_animationVelocity.Value/30);
                rigidbody2D.velocity = _preAnimationVelocity.Value*-1f;
                IsInHole = false;
                _preAnimationVelocity = null;
                _animationVelocity = null;
                DeltaAnimationTime = null;
                break;
        }
    }

    private void AttachToVRail()
    {
        var velocity = new Vector2(0, rigidbody2D.velocity.y);
        rigidbody2D.velocity = velocity;
    }

    private void AttachToHRail()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
    }

    private void TakeLethalDamage()
    {
        TakeDamage(Health);
    }

    private void TakeDamage(int damage = 1)
    {
        Health -= damage;
        GetComponent<Animator>().SetTrigger("DamageTaken");
        if (Health <= 0)
            PlayerDied();   
    }

    private void PlayerDied()
    {
        GetComponent<Animator>().SetTrigger("LevelComplete");
    }

    private void CollideRail()
    {
        if (IsOnHRail && rigidbody2D.velocity.y != 0) AttachToHRail();
        else if (IsOnVRail && rigidbody2D.velocity.x != 0) AttachToVRail();
        if (IsOnVRail)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                IsOnVRail = false;
                rigidbody2D.velocity = new Vector2(-Speed, rigidbody2D.velocity.y);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                IsOnVRail = false;
                rigidbody2D.velocity = new Vector2(Speed, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y <= 0 ? -Speed : Speed);
            }
        }
        else if (IsOnHRail)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                IsOnHRail = false;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Speed);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                IsOnHRail = false;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -Speed);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x <= 0 ? -Speed : Speed, 0);
            }
        }
    }

    private void NormalizeVelocity()
    {
        if (OnRail) 
            return;

        var velocity = rigidbody2D.velocity;
        if (velocity.x <= 0)
            velocity.x = -Speed;
        else velocity.x = Speed;

        if (velocity.y <= 0)
            velocity.y = -Speed;
        else velocity.y = Speed;

        rigidbody2D.velocity = velocity;
    }
}
