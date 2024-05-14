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

    [SerializeField] private Door _leftDoor;
    [SerializeField] private Door _rightDoor;

    [SerializeField] private BossHealthbar _bossHealthbar;

    [SerializeField] private List<BoxCollider2D> _attackTrigger;

    [SerializeField] private GameObject shockWave;

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

    public BoxCollider2D GetAttackTrigger(int trigger)
    {
        return _attackTrigger[trigger];
    }

    public void StartShockwave()
    {
        GameObject go = Instantiate(shockWave, transform);
    }
}
