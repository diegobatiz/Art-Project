using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
	[SerializeField] protected MovementData _moveData;
	protected float _maxSpeed;
	protected float _accelAmount;
	protected float _decelAmount;

    protected virtual void Awake()
    {
		_maxSpeed = _moveData.MaxSpeed;
		_accelAmount = _moveData.AccelAmount;
		_decelAmount = _moveData.DecelAmount;
    }

    protected virtual void FixedUpdate()
    {
		float targetSpeed = GetDirection() * _maxSpeed;

		float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _accelAmount : _decelAmount;

		float speedDif = targetSpeed - _rb.velocity.x;

		float movement = speedDif * accelRate;

		_rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

	protected abstract float GetDirection();

	protected abstract void Flip(float dir);
}
