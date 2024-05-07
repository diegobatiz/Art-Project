using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public bool CanWalk { get; set; } = true;
    private float _direction = 1f;

    private void SetDirection(float direction)
    {
        _direction = direction;
    }

    protected override float GetDirection()
    {
        if (!CanWalk)
        {
            return 0.0f;
        }
        return _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _direction *= -1f;

            Flip(_direction);
        }
    }
    protected override void Flip(float dir)
    { 
        if (dir > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
}
