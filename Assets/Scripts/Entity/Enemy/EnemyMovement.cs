using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private float _direction = 1f;

    protected override float GetDirection()
    {
        return _direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    protected override void Flip(int dir)
    {
       
    }
}
