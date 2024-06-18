using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public bool IsAttacking { get; private set; } = false;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _rotatePoint;
    [SerializeField] private Vector2 _attackSize;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _knockbackForce;

    [Header("DEBUG")]
    [SerializeField] private GameObject _sprite;

    private Vector2 _aimInput;
    private float _angle;
    private float _attackButton;
    private bool _canAttack = true;
    private List<Rigidbody2D> _hitObjects;
    private float _knockbackDir;
    private Timer _timer;

    private void Awake()
    {
        _timer = new Timer(_attackDuration);
        _timer.OnTimerEnd += EndAttack;
        _hitObjects = new List<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsAttacking)
        {
            Collider2D[] collisions = Physics2D.OverlapCapsuleAll(_attackPoint.position, _attackSize, CapsuleDirection2D.Horizontal, _angle, _layerMask);

            foreach (var collision in collisions)
            {
                if (_hitObjects.Contains(collision.attachedRigidbody))
                {
                    continue;
                }

                _hitObjects.Add(collision.attachedRigidbody);

                IHealth damageable = collision.GetComponent<IHealth>();

                damageable.Damage(_attackDamage);
                Debug.Log($"Hit for { _attackDamage} damage");

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

                EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
                Debug.Log("YOOO");
                float knockBack = enemy.KnockbackMultiplier;
                collision.attachedRigidbody?.AddForce(new Vector2(_knockbackDir * _knockbackForce * knockBack, 0));
            }

            _timer.Tick(Time.deltaTime);
        }

        if (_attackButton == 0.0f)
        {
            _canAttack = true;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        _attackButton = context.ReadValue<float>();

        if (IsAttacking || !_canAttack)
        {
            return;
        }
        if (transform.localScale.x != 1f && _aimInput.y != 0f && _aimInput.x != 0f)
        {
            _angle = Mathf.Atan2(-_aimInput.y, -_aimInput.x);
        }
        else
        {
            _angle = Mathf.Atan2(_aimInput.y, _aimInput.x);
        }
        _rotatePoint.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * _angle);

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

    public void Aim(InputAction.CallbackContext context)
    {
        _aimInput = context.ReadValue<Vector2>();
    }
}
