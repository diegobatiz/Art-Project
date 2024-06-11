using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround
{
    private float _range;
    private Transform _rayPoint;
    private LayerMask _collisionMask;

    public CheckGround(float range, Transform ray, LayerMask mask)
    {
        _range = range;
        _rayPoint = ray;
        _collisionMask = mask;
    }

    public bool IsOverEdge()
    {
        Vector2 endPos = _rayPoint.position + (Vector3.down * _range);

        RaycastHit2D raycast = Physics2D.Linecast(_rayPoint.position, endPos, _collisionMask);

        Debug.DrawLine(_rayPoint.position, endPos, Color.yellow);

        if (raycast.collider == null)
        {
            return true;
        }

        return false;

    }
}
