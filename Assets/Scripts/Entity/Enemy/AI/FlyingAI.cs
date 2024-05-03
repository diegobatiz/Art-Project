using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : MonoBehaviour
{
    [SerializeField] private EnemyData _ai;
    private Timer _seeTargetTimer;

    public bool SeePlayer { get; set; }
    public Vector2 PlayerDirection { get; set; }

    private void Awake()
    {
        _seeTargetTimer = new Timer(_ai.FocusDuration);
        _seeTargetTimer.OnTimerEnd += LosePlayer;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _ai.Range, Vector2.right, 0, _ai.LayerMask);

        if (hit)
        {
            Debug.Log("I SEE HIM");
            Vector2 playerPos = hit.collider.ClosestPoint(transform.position);
            PlayerDirection = (playerPos - (Vector2)transform.position).normalized;
            SeePlayer = true;

            _seeTargetTimer.ResetTimer();
        }
        else if (SeePlayer)
        {
            _seeTargetTimer.Tick(Time.deltaTime);
        }
    }

    private void LosePlayer()
    {
        SeePlayer = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _ai.Range);
    }
}
