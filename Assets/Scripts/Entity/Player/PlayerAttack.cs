using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private bool _isAttacking = false;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector2 _attackSize;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _knockbackForce;

    [Header("DEBUG")]
    [SerializeField] private GameObject _sprite;

    private float _knockbackDir;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_attackDuration);
        _timer.OnTimerEnd += EndAttack;
    }

    private void Update()
    {
        if (_isAttacking)
        {
            Collider2D[] collisions = Physics2D.OverlapCapsuleAll(_attackPoint.position, _attackSize, CapsuleDirection2D.Horizontal, 0, _layerMask);

            foreach(var collision in collisions)
            {
                IHealth damageable = collision.GetComponent<IHealth>();

                damageable.Damage(_attackDamage);

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

                collision.attachedRigidbody.AddForce(new Vector2(_knockbackDir * _knockbackForce, 0));

            }

            _timer.Tick(Time.deltaTime);        
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (_isAttacking)
        {
            return;
        }

        _sprite.SetActive(true);
        _isAttacking = true;
    }

    private void EndAttack()
    {
        _sprite.SetActive(false);
        _isAttacking = false;
        _timer.ResetTimer();
    }
}
