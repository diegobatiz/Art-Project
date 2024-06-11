using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RhinoState
{
    Awake,
    Walk,
    ShortAttack,
    ChargeAttack,
    StompAttack,
    Dying,
    None
};

public class RhinoBoss : MonoBehaviour, IBoss
{
    private StateMachine<RhinoBoss> _stateMachine;
    private RhinoState _currentState;

    [SerializeField] private Vector2 _knockBack;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private MovementData _chargeData;
    [SerializeField] private MovementData _walkData;

    [SerializeField] private Door _leftDoor;
    [SerializeField] private Door _rightDoor;

    [SerializeField] private BossHealthbar _bossHealthbar;

    [SerializeField] private List<BoxCollider2D> _attackTrigger;

    [SerializeField] private GameObject shockWave;
    [SerializeField] private MainLight _light;

    [field: SerializeField]
    public float MaxWalkTime { get; private set; }
    [field: SerializeField]
    public float MinWalkTime { get; private set; }

    private Player _player;


    private void Awake()
    {
        _currentState = RhinoState.None;

        _stateMachine = new StateMachine<RhinoBoss>(this);
        _stateMachine.AddState<RhinoAwake>();
        _stateMachine.AddState<RhinoWalk>();
        _stateMachine.AddState<RhinoShortAttack>();
        _stateMachine.AddState<RhinoCharge>();
        _stateMachine.AddState<RhinoShockwave>();

        _health.OnDeath += RhinoDead;

        ChangeState(RhinoState.None);
    }

    private void Update()
    {
        if (_currentState != RhinoState.None)
        {
            _stateMachine.Update(Time.deltaTime);
        }
    }

    public void ChangeState(RhinoState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState = state;
        _stateMachine.ChangeState((int)_currentState);
    }

    private void RhinoDead()
    {
        //play an animation here
        _leftDoor.Open();
        _rightDoor.Open();
        _bossHealthbar.gameObject.SetActive(false);
        _light.MakeLightNormal();
    }

    public RhinoState GetLastState()
    {
        return (RhinoState)_stateMachine.GetLastState();
    }

    public void ShowHealthBar()
    {
        Debug.Log("poof, healthbar appear");
        _bossHealthbar.gameObject.SetActive(true);
    }

    public void CloseDoors()
    {
        _leftDoor.Close();
        _rightDoor.Close();
    }

    public void ActivateBoss(Player player)
    {
        _player = player;
        ChangeState(RhinoState.Awake);
    }

    public Vector2 FindPlayer()
    {
        return _player.transform.position;
    }

    public void SetDirecton(int direction)
    {
        _movement.SetDirection(direction);
    }

    public BoxCollider2D GetAttackTrigger(int trigger)
    {
        return _attackTrigger[trigger];
    }

    public Rigidbody2D GetRB()
    {
        return _rb;
    }

    public void StartShockwave()
    {
        GameObject go = Instantiate(shockWave, transform);
        go.transform.localPosition = Vector3.zero;
    }

    public void StartCharge()
    {
        _movement.ChangeMoveData(_chargeData);
        _movement.CanWalk = true;
        _movement.OnHitWall += EndCharge;

        int direction = FindPlayer().x > transform.position.x ? 1 : -1;
        _movement.SetDirection(direction);
        Debug.Log("start charge");
    }

    public void EndCharge()
    {
        _movement.CanWalk = false;
        _movement.ChangeMoveData(_walkData);
        _movement.OnHitWall -= EndCharge;

        float knockbackX = _knockBack.x * _movement.GetDir() * -1f;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(knockbackX, _knockBack.y));

        ChangeState(RhinoState.Walk);
    }
}
