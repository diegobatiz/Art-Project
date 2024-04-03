using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Movement
{
    [SerializeField] private MovementData _chargeData;
    public float Direction { get; private set; } = 1f;

    protected override void FixedUpdate()
    {
        //if enemy detects player, stop for 0.5 sec and face player
        base.FixedUpdate();
    }

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

    protected override void Flip(int dir)
    {
        return ;
    }
}
