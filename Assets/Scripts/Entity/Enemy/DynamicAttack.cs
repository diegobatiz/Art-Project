using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAttack : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private LayerMask _mask;

    [SerializeField] private float _damage;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _airKnockbackForce;
    private float _knockbackDir;

    private Vector2 _size;

    private void Awake()
    {
        _size = _collider.size;
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, _size, 0, _mask);
        if (hit == null)
        {
            return; 
        }

        if (hit.gameObject.CompareTag("Player"))
        {
            IHealth player = hit.gameObject.GetComponent<IHealth>();
            if (!player.Damage(_damage))
            {
                return;
            }
            Vector3 collisionPoint = hit.ClosestPoint(transform.position);
            Vector3 collisionNormal = transform.position - collisionPoint;
            if (collisionNormal.x > 0)
            {
                _knockbackDir = -1f;
            }
            else
            {
                _knockbackDir = 1f;
            }

            hit.attachedRigidbody.velocity = Vector2.zero;
            hit.attachedRigidbody.AddForce(new Vector2(_knockbackDir * _knockbackForce, _airKnockbackForce));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, _size);
    }
}
