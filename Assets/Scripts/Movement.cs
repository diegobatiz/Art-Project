using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
	[SerializeField] protected MovementData _moveData;

    protected virtual void FixedUpdate()
    {
		float targetSpeed = GetDirection() * _moveData.MaxSpeed;


		float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _moveData.AccelAmount : _moveData.DecelAmount;

		float speedDif = targetSpeed - _rb.velocity.x;

		float movement = speedDif * accelRate;

		_rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

	protected abstract float GetDirection();

}
