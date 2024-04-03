using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _damage;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _airKnockbackForce;
    private float _knockbackDir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IHealth player = collision.gameObject.GetComponent<IHealth>();
            player.Damage(_damage);
            Vector3 collisionPoint = collision.ClosestPoint(transform.position);
            Vector3 collisionNormal = transform.position - collisionPoint;
            if (collisionNormal.x > 0)
            {
                _knockbackDir = -1f;
            }
            else
            {
                _knockbackDir = 1f;
            }

            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.attachedRigidbody.AddForce(new Vector2(_knockbackDir * _knockbackForce, _airKnockbackForce));
        }
    }
}
