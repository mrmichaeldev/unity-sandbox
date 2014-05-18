using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    // Use this for initialization
    public bool IsOnHRail;
    public bool IsOnVRail;
    public int NumPickups;
    private bool IsInHole;
    public bool IsLevelComplete;
    public bool OnRail { get { return IsOnHRail || IsOnVRail; } }
    public float? DeltaAnimationTime;

    //Fields for controlling player when entering objective
    private GameObject _hole;
    private Vector2? _preAnimationVelocity = null;
    private Vector2? _animationVelocity = null;
    private const float _animationDelay = 0.6f;
    private CircleCollider2D _collider;


    void Awake()
    {
        Application.targetFrameRate = 60;
        rigidbody2D.velocity = new Vector2(Speed, Speed);
        _collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if within .5 radius from hole then make this kinematic and do animation thingy

        //TODO: figure out how to do this better?
        if (_hole == null)
        {
            _hole = GameObject.FindWithTag("Hole");
        }

        //if (Mathf.Abs(transform.position.x - _hole.transform.position.x) <= 0.5f && Mathf.Abs(transform.position.y - _hole.transform.position.y) <= 0.5f)
        //{
        //    if (Mathf.Sqrt(Mathf.Pow(transform.position.x - _hole.transform.position.x, 2) + Mathf.Pow(transform.position.x - _hole.transform.position.x, 2)) <= 0.5f)
        //    {

        //        //GetComponent<Animator>().trans
        //    }
        //}
        //else
        //{
        //    rigidbody2D.isKinematic = IsKinematic = false;
        //    CollideRail();
        //}

        if (!IsInHole)
        {
            CollideRail();
            NormalizeVelocity();
        }
        else
        {
            if (DeltaAnimationTime != null)
            {
                DeltaAnimationTime += Time.deltaTime;
                if (DeltaAnimationTime >= _animationDelay)
                {
                    _animationVelocity = rigidbody2D.velocity = _animationVelocity.Value * -1;
                    DeltaAnimationTime = null;
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
        if (collider.tag == "Hole")
        {
            IsInHole = true;
            if (IsLevelComplete)
            {
                GetComponent<Animator>().SetTrigger("LevelComplete");
            }
            else
            {
                DeltaAnimationTime = 0;
                _preAnimationVelocity = rigidbody2D.velocity;
                _animationVelocity = new Vector2(_hole.transform.position.x - transform.position.x, _hole.transform.position.y - transform.position.y) / 0.6f;
                rigidbody2D.velocity = _animationVelocity.Value;
                GetComponent<Animator>().SetTrigger("LevelNotComplete");
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Hole")
        {
            //transform.Translate(_animationVelocity.Value * 5f);
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
