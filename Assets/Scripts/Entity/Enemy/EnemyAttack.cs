using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _knockbackForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(_damage);
            Vector2 direction = collision.GetContact(0).normal;
            if (direction.y > 0.5f)
            {
                collision.rigidbody.AddForce(new Vector2(0f, direction.y * _knockbackForce));
            }
            else
            {
                collision.rigidbody.AddForce(new Vector2(-direction.x * _knockbackForce, 0f));
            }
        }
    }
}
