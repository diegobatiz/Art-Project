using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    [field:SerializeField] public bool CanWalk { get; set; } = true;
    [SerializeField] private float _direction = 1f;
    [SerializeField] private bool _destroyOnTouch = false;

    public void SetDirection(float direction)
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
            if (_destroyOnTouch)
            {
                Destroy(gameObject);
            }
            else
            {
                _direction *= -1f;

                Flip(_direction);
            }
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
