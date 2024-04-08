using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    [SerializeField] private Player _player;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private JumpData _jumpData;
    [SerializeField] private float _maxFallSpeed;
 
    public bool IsJumping { get; private set; }
    private bool _canJump;
    private float _jumpBufferTime;
    private float _coyoteTime;
    private float _gravityScale;

    private float _direction;
    private float _jumpInput;

    protected override void Awake()
    {
        base.Awake();
        _gravityScale = _rb.gravityScale;

        _player.OnDamaged(EndJump);
    }

    private void Update()
    {
        if (IsGrounded())
        {
            _coyoteTime = _jumpData.MaxCoyoteTime;
        }
        else
        {
            _coyoteTime -= Time.deltaTime;
        }

        if (_jumpInput > 0.5f && _canJump)
        {
            _jumpBufferTime = _jumpData.MaxJumpBufferTime;
        }
        else
        {
            _jumpBufferTime -= Time.deltaTime;
        }

        if (_jumpBufferTime > 0f && _coyoteTime > 0f && _canJump)
        {
            _jumpBufferTime = 0f;
            _coyoteTime = 0f;
            StartJump();
        }
        else
        {
            JumpUpdate();
        }

        if (_rb.velocity.y < -_maxFallSpeed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_maxFallSpeed);
        }

        if (_jumpInput == 0f)
        {
            _canJump = true;
        }
    }

    private void StartJump()
    {
        IsJumping = true;
        _canJump = false;
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, 0f);
        _rb.gravityScale = _jumpData.GravityScaleFactor;
           
        _rb.AddForce(_rb.mass * Vector2.up * _jumpData.InitJumpSpeed, ForceMode2D.Impulse);
    }

    private void JumpUpdate()
    {
        if (!IsJumping)
        {
            return;
        }

        if (_jumpInput == 0f && _rb.velocity.y > 0f)
        {
            _rb.gravityScale = _jumpData.GravityScaleFactor * _jumpData.EarlyExitGravityFactor;
        }
        else if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _jumpData.GravityScaleFactor * _jumpData.FallFasterGravityFactor;
        }
        else
        {
            _rb.gravityScale = _jumpData.GravityScaleFactor;
        }


        if (IsGrounded() && _rb.velocity.y <= 0.0f)
        {
            EndJump(0);
        }
    }

    public bool IsGrounded()
    {
        return _groundCheck.IsGrounded();
    }

    public void EndJump(float amt)
    {
        IsJumping = false;
        _rb.gravityScale = _gravityScale;
    }

    protected override float GetDirection()
    {
        return _direction;
    }

    protected override void Flip(int dir)
    {
        transform.localScale = new Vector3(dir, 1f, 1f);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<float>();

        if (_player?.IsAttacking() != false)
        {
            return;
        }

        int flip = (int)_direction;
        if (flip != 0)
        {
            Flip(flip);
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        _jumpInput = context.ReadValue<float>();
    }
}
