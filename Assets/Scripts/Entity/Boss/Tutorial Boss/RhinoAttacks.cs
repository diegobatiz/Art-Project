using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoWalk : IState<RhinoBoss>
{
    private RhinoBoss _rhino;
    private EnemyMovement _rhinoMove;

    private float _stateTime;
    private float _maxTime;
    private float _minTime;

    private bool _hasBeenEntered = false;

    Timer _walkTimer;
    Timer _restTimer;

    public void Enter(RhinoBoss agent)
    {
        if (!_hasBeenEntered)
        {
            _rhino = agent;
            _rhinoMove = agent.GetComponent<EnemyMovement>();

            _maxTime = _rhino.MaxWalkTime;
            _minTime = _rhino.MinWalkTime;

            _hasBeenEntered = true;

            _restTimer = new Timer(2);
            _restTimer.OnTimerEnd += StartWalk;
        }
        else
        {
            _restTimer.ResetTimer();
        }

        _stateTime = Random.Range(_minTime, _maxTime);
        _walkTimer = new Timer(_stateTime);
        _walkTimer.OnTimerEnd += EnterAttackState;

        if (agent.GetLastState() != RhinoState.ChargeAttack)
        {
            StartWalk();
        }
    }

    public void Exit(RhinoBoss agent)
    {
        _rhinoMove.CanWalk = false;
    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
        if (agent.GetLastState() == RhinoState.ChargeAttack && _restTimer.RemainingSeconds > 0)
        {
            _restTimer.Tick(deltaTime);
        }
        else
        {
            _walkTimer.Tick(deltaTime);
        }
        //Debug.Log(_walkTimer.RemainingSeconds);
    }

    private void EnterAttackState()
    {
        if (Vector2.Distance(_rhino.FindPlayer(), _rhino.transform.position) < 2)
        {
            _rhino.ChangeState(RhinoState.ShortAttack);
            return;
        }
        int nextState = Random.Range(2, 5);
        _rhino.ChangeState((RhinoState)nextState);
    }

    private void StartWalk()
    {
        int direction = _rhino.FindPlayer().x > _rhino.transform.position.x ? 1 : -1;
        _rhinoMove.SetDirection(direction);
        _rhinoMove.CanWalk = true;
    }
}

public class RhinoShortAttack : IState<RhinoBoss>
{
    private RhinoBoss _agent;

    private BoxCollider2D _trigger;
    private Rigidbody2D _rb;

    private bool _hasBeenEntered;
    private Timer _waitTimer;
    private bool _wait;
    private Timer _attackTimer;
    private bool _attack;

    private int _dir;

    public void Enter(RhinoBoss agent)
    {
        Debug.Log("Enter Short Attack State");
        if (!_hasBeenEntered)
        {
            _agent = agent;
            _rb = agent.GetRB();
            _hasBeenEntered = true;
            _trigger = agent.GetAttackTrigger(0);

            _waitTimer = new Timer(0.5f);
            _waitTimer.OnTimerEnd += StartAttack;

            _attackTimer = new Timer(1f);
            _attackTimer.OnTimerEnd += EndAttack;
        }
        else
        {
            _waitTimer.ResetTimer();
            _attackTimer.ResetTimer();
        }

        _dir = _agent.FindPlayer().x > _agent.transform.position.x ? 1 : -1;
        _agent.SetDirecton(_dir);
        _wait = true;
        _attack = false;
    }

    public void Exit(RhinoBoss agent)
    {
        _trigger.gameObject.SetActive(false);
    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
        if (_wait)
        {
            _waitTimer.Tick(deltaTime);
        }
        else if (_attack)
        {
            _attackTimer.Tick(deltaTime);
        }
    }

    private void StartAttack()
    {
        _attack = true;
        _wait = false;

        _rb.velocity = Vector3.zero;
        _rb.AddForce(new Vector2(500 * _dir, 0));

        _trigger.gameObject.SetActive(true);
    }

    private void EndAttack()
    {
        _trigger.gameObject.SetActive(false);

        _agent.ChangeState(RhinoState.Walk);
    }
}

public class RhinoCharge : IState<RhinoBoss>
{
    private RhinoBoss _agent;

    private BoxCollider2D _trigger;

    private bool _hasBeenEntered;
    private Timer _waitChargeTimer;
    bool _waitCharge;

    public void Enter(RhinoBoss agent)
    {
        Debug.Log("Enter Charge Attack State");
        if (!_hasBeenEntered)
        {
            _agent = agent;
            _hasBeenEntered = true;
            _trigger = agent.GetAttackTrigger(1);
            _waitChargeTimer = new Timer(1f);
            _waitChargeTimer.OnTimerEnd += StartCharge;
        }
        else
        {
            _waitChargeTimer.ResetTimer();
        }

        _waitCharge = true;
    }

    public void Exit(RhinoBoss agent)
    {
        _trigger.gameObject.SetActive(false);
    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
        if (_waitCharge)
        {
            _waitChargeTimer.Tick(deltaTime);
        }
    }

    private void StartCharge()
    {
        _agent.StartCharge();
        _trigger.gameObject.SetActive(true);
    }
}

public class RhinoShockwave : IState<RhinoBoss>
{
    private RhinoBoss _agent;

    private bool _hasBeenEntered;
    bool _startAttack = false;
    private Timer _startAttackTime;
    private Timer _waitTime;

    public void Enter(RhinoBoss agent)
    {
        Debug.Log("Enter Shockwave Attack State");
        if (!_hasBeenEntered)
        {
            _agent = agent;
            _hasBeenEntered = true;
            _startAttackTime = new Timer(1f);
            _waitTime = new Timer(1.5f);
            _startAttackTime.OnTimerEnd += StartAttack;
            _waitTime.OnTimerEnd += ChangeToWalkState;
        }
        else
        {
            _startAttackTime.ResetTimer();
            _waitTime.ResetTimer();
        }
    }

    public void Exit(RhinoBoss agent)
    {

    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
        if (!_startAttack)
        {
            //move rhino to the center with animation
            _startAttackTime.Tick(deltaTime);
            //Debug.Log(_startAttackTime.RemainingSeconds);
        }
        else
        {
            _waitTime.Tick(deltaTime);
            //Debug.Log(_waitTime.RemainingSeconds);
        }
    }

    private void StartAttack()
    {
        _startAttack = true;
        _agent.StartShockwave();
        Debug.Log("Start Shockwave");
    }

    private void ChangeToWalkState()
    {
        _startAttack = false;
        _agent.ChangeState(RhinoState.Walk);
        Debug.Log("Change to walk");
    }
}