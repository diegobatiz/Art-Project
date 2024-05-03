using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private FlyingAI _flyAI;
    [SerializeField] private MovementData _moveData;
    [SerializeField] private Rigidbody2D _rb2d;

    private float _maxSpeed;
    private float _accelAmount;
    private float _decelAmount;

    protected virtual void Awake()
    {
        _maxSpeed = _moveData.MaxSpeed;
        _accelAmount = _moveData.AccelAmount;
        _decelAmount = _moveData.DecelAmount;
    }

    private void FixedUpdate()
    {
        Vector2 targetSpeed = Vector2.zero;

        if (!_flyAI.SeePlayer && _rb2d.velocity != Vector2.zero)
        {
            Vector2 dir = _rb2d.velocity.normalized;

            targetSpeed = -dir * _maxSpeed;
        }
        else if (_flyAI.SeePlayer)
        {
            Vector2 dir = _flyAI.PlayerDirection;

            targetSpeed = dir * _maxSpeed;
        }

        Vector2 speedDif = targetSpeed - _rb2d.velocity;

        float accel = _flyAI.SeePlayer ? _accelAmount : _decelAmount;

        Vector2 movement =  speedDif * accel;

        _rb2d.AddForce(movement, ForceMode2D.Force);
    }
}
