using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI 
{
    private Transform _raycastPoint;
    private Transform _enemyPos;
    private LayerMask _layerMask;
    private float _sightRange;

    private Timer _seeTargetTimer;
    private float _focusTime;

    public bool SeePlayer { get; private set; }
    public float PlayerDirection;

    public EnemyAI(float range, Transform ray, Transform enemy, LayerMask mask, float focusTime = 1.0f)
    {
        _sightRange = range;
        _raycastPoint = ray;
        _enemyPos = enemy;
        _layerMask = mask;
        _focusTime = focusTime;

        _seeTargetTimer = new Timer(_focusTime);
        _seeTargetTimer.OnTimerEnd += LoseTarget;
    }

    public bool DetectPlayer()
    {
        Vector2 endPos = _enemyPos.position + (_enemyPos.right * _sightRange);

        RaycastHit2D raycast = Physics2D.Linecast(_enemyPos.position, endPos, _layerMask);

        Debug.DrawRay(_enemyPos.position, endPos, Color.yellow);

        if (raycast.collider != null)
        {
            float colliderPos = raycast.collider.ClosestPoint(_enemyPos.position).x;
            PlayerDirection = colliderPos > _raycastPoint.position.x ? 1 : -1;
            SeePlayer = true;

            _seeTargetTimer.ResetTimer();
            return true;
        }
        else if (SeePlayer)
        {
            _seeTargetTimer.Tick(Time.deltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LoseTarget()
    {
        SeePlayer = false;
    }
}
