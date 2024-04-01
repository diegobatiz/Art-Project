using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public float Direction { get; private set; } = 1f;

    protected override float GetDirection()
    {
        if (DetectPlayer())
        {
            return 0;
        }
        else
        {
            return Direction;
        }
    }

    private bool DetectPlayer()
    {
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Direction *= -1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
