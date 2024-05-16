using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public bool IsAttacking { get; private set; } = false;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector2 _attackSize;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _knockbackForce;

    [Header("DEBUG")]
    [SerializeField] private GameObject _sprite;

    private bool _canAttack;
    private List<Collider2D> _hitObjects;
    private float _knockbackDir;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_attackDuration);
        _timer.OnTimerEnd += EndAttack;
        _hitObjects = new List<Collider2D>();
    }

    private void Update()
    {
        if (IsAttacking)
        {
            Collider2D[] collisions = Physics2D.OverlapCapsuleAll(_attackPoint.position, _attackSize, CapsuleDirection2D.Horizontal, 0, _layerMask);

            foreach(var collision in collisions)
            {
                if (_hitObjects.Contains(collision))
                {
                    continue;
                }

                _hitObjects.Add(collision);

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

                if (damageable.Equals(typeof(EnemyHealth)))
                {
                    EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
                    Debug.Log("YOOO");
                    float knockBack = enemy.KnockbackMultiplier;
                    collision.attachedRigidbody?.AddForce(new Vector2(_knockbackDir * _knockbackForce * knockBack, 0));
                }
            }

            _timer.Tick(Time.deltaTime);        
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        float attack = context.ReadValue<float>();

        if (attack == 0.0f)
        {
            _canAttack = true;
        }

        if (IsAttacking || !_canAttack)
        {
            return;
        }

        _sprite.SetActive(true);
        IsAttacking = true;
        _canAttack = false;
    }

    private void EndAttack()
    {
        _sprite.SetActive(false);
        IsAttacking = false;
        _timer.ResetTimer();
        _hitObjects.Clear();
    }
}
