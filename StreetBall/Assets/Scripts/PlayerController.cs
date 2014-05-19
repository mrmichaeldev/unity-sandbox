using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

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
    private Vector2? _preAnimationVelocity = null;
    private Vector2? _animationVelocity = null;
    private const float _animationDelay = 0.6f;
    private CircleCollider2D _collider;


    void Start()
    {
        Application.targetFrameRate = 60;
        rigidbody2D.velocity = new Vector2(Speed, Speed);
        _collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
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
                        _animationVelocity = rigidbody2D.velocity = _animationVelocity.Value * -1f;
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

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            IsOnVRail = IsOnHRail = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Hole":
                {
                    if (collider.tag == "Hole")
                    {
                        IsInHole = true;
                        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), true);
                        if (IsLevelComplete)
                        {
                            if (DeltaAnimationTime == null && _animationVelocity == null)
                            {
                                DeltaAnimationTime = 0;
                                _animationVelocity = rigidbody2D.velocity = new Vector2(_hole.transform.position.x - transform.position.x, _hole.transform.position.y - transform.position.y) / 0.6f;
                                GetComponent<Animator>().SetTrigger("LevelComplete");
                            }
                        }
                        else
                        {
                            if (DeltaAnimationTime == null && _animationVelocity == null)
                            {
                                DeltaAnimationTime = 0;
                                _preAnimationVelocity = rigidbody2D.velocity;
                                _animationVelocity = rigidbody2D.velocity = new Vector2(_hole.transform.position.x - transform.position.x, _hole.transform.position.y - transform.position.y) / 0.6f;
                                GetComponent<Animator>().SetTrigger("LevelNotComplete");
                            }
                        }
                    }
                }
                break;

            case "Bullet":
                {
                    Health--;
                    collider.GetComponent<BulletController>().ShouldDestroy = true;
                }
                break;
            default:
                break;
        }    
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Hole")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemies"), false);
            transform.Translate(_animationVelocity.Value / 30);
            rigidbody2D.velocity = _preAnimationVelocity.Value * -1f;
            IsInHole = false;
            _preAnimationVelocity = null;
            _animationVelocity = null;
            DeltaAnimationTime = null;
        }
    }

    void AttachToVRail()
    {
        var velocity = new Vector2(0, rigidbody2D.velocity.y);
        rigidbody2D.velocity = velocity;
    }

    void AttachToHRail()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
    }

    void CollideRail()
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
        if (!OnRail)
        {
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
}
