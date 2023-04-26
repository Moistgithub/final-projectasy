using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    //public exposed variables for tweaking 
    public float Acceleration = 10f;
    public float Jumpforce = 50f;
    public LayerMask GroundLayerMask;
    public float MaxSlopeAngle = 45f;
    public PhysicsMaterial2D Default;
    public PhysicsMaterial2D FullFriction;
    public PhysicsMaterial2D nostickyair;
    public cooldown coyotetime;
    public bool IsGrounded = true;
    public float GroundCheckRadius = 0f;
    public bool IsUp = false;
    public bool IsDown = false;
    public float knockbackforce = 5f;
    public bool IsRunning
    {
        get
        {
            return _isrunning;
        }
    }
    public bool FlipAnim
    {
        get
        {
            return _FlipAnim;
        }
        set
        {
            _FlipAnim = value;  
        }
    }
    //protected varaibles
    protected Rigidbody2D _rigidbody;
    protected Vector2 _inputDirection;
    protected RaycastHit2D _groundhit;
    protected RaycastHit2D _slopehit;
    protected float _lastslopeangle;

    public float _slopeangle = 0f;
    protected Vector2 _slopehitNormal = Vector2.zero;
    protected bool _isonslope = false; 
    protected bool _canwalkonslope = false;
    protected bool _isrunning = false;
    protected bool _FlipAnim = false;
    protected bool IsJumping = false;
    protected bool _canjump = true;
    private health _health;
    private bool disableinput = false;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<health>();
        if (_health != null)
        {
            _health.onhit += hit;
            _health.onhitreset += ResetMove;
        }
    }
    private void ResetMove()
    {
        Debug.Log("should reeset");
        disableinput = false;
    }
    private void hit(GameObject source)
    {
        float pushhorizontal = 0f;
        if (source != null)
        {
            if (source.transform.position.x < transform.position.x)
            {
                pushhorizontal = knockbackforce;
            }
            else
            {
                pushhorizontal = -knockbackforce;
            }
        }
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity = new Vector2(pushhorizontal, knockbackforce);
        disableinput = true;
    }

    private void OnDisable()
    {
        if(_health != null)
        {
            _health.onhit -= hit;
            _health.onhitreset -= ResetMove;
        }
    }
    void Update()
    {
        HandleInput();

    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckSlope();
        HandleMovement();
        HandleFlip();
    }
    protected virtual void HandleInput()
    {

    }
    protected virtual void HandleMovement()
    {
        if (disableinput)
            return;
        if (_rigidbody == null)
            return;

        _rigidbody.velocity = new Vector2(_inputDirection.x * Acceleration, _rigidbody.velocity.y);
        if (_rigidbody.velocity.x ==0)
        {
            _isrunning = false;
        }
        else
        {
            _isrunning = true;
        }
    }
    protected virtual void HandleFlip()
    {
        if (_inputDirection.x == 0)
            return;
        if (_inputDirection.x > 0)
        {
            FlipAnim = false;
        }
        else if (_inputDirection.x < 0)
        {
            FlipAnim = true;
        }
    }
    protected virtual void DoJump()
    {
        if (disableinput)
            return;
        if (_rigidbody == null)
            return;

        if (!_canjump)
            return;

        if (coyotetime.CurrentProgress == cooldown.Progress.Finished)
            return; 

        _canjump = false;
        IsJumping = true;

        Debug.Log("Jumping");
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Jumpforce);
        coyotetime.StopCooldown();

    }

    public Transform groundcheckpoint;
    protected void CheckGround()
    {
        IsGrounded = Physics2D.OverlapCircle(groundcheckpoint.position, GroundCheckRadius, GroundLayerMask);

        if (_rigidbody.velocity.y <= 0f)
        {
            IsJumping = false;
            // CoyoteTime.StopCooldown();;
        }

        if (IsGrounded && !IsJumping)
        {
            _canjump = true;
            if (coyotetime.CurrentProgress != cooldown.Progress.Ready)
                coyotetime.StopCooldown(); ;

            // DoJump();

        }

        if (!IsGrounded && !IsJumping && coyotetime.CurrentProgress == cooldown.Progress.Ready)
        {
            coyotetime.StartCooldown();
        }
    }
    protected void CheckSlope()
    {
        _slopehit = Physics2D.Raycast(transform.position, Vector2.down, 1f, GroundLayerMask);

        if (_slopehit)
        {
            _isonslope = true;
            _slopehitNormal = _slopehit.normal;
            _slopeangle = Vector2.Angle(Vector2.up, _slopehitNormal);
            //Debug.DrawRay(_slopehit.point, _slopehit.normal, Color.blue, 1f);
            //Debug.Log(Vector2.Angle(Vector2.up, _slopehit.normal));
            if (_slopeangle != _lastslopeangle)
            {
                _isonslope = true;
            }
            if (_slopeangle <1)
            {
                _isonslope = false;
            }
            _lastslopeangle = _slopeangle;
        }
  
        if (_slopeangle > MaxSlopeAngle)
        {
            _canwalkonslope = false;
        }
        else
        {
            _canwalkonslope = true;
        }

        if (/*_isonslope && _canwalkonslope &&*/ _inputDirection.x == 0)
        {
            _rigidbody.sharedMaterial = FullFriction;
        }
        else if (!IsGrounded)
        {
            _rigidbody.sharedMaterial = nostickyair;
        }
        else
        {
            _rigidbody.sharedMaterial = Default;
        }
    }
}
