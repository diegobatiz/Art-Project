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

    public void Enter(RhinoBoss agent)
    {
        if (!_hasBeenEntered)
        {
            _rhino = agent;
            _rhinoMove = agent.GetComponent<EnemyMovement>();

            _maxTime = _rhino.MaxWalkTime;
            _minTime = _rhino.MinWalkTime;

            _hasBeenEntered = true;
        }

        int direction = _rhino.FindPlayer().x > _rhino.transform.position.x ? 1 : -1;
        _rhinoMove.SetDirection(direction);

        _stateTime = Random.Range(_minTime, _maxTime);
        _walkTimer = new Timer(_stateTime);
        _walkTimer.OnTimerEnd += EnterAttackState;
        _rhinoMove.CanWalk = true;
    }

    public void Exit(RhinoBoss agent)
    {
        _rhinoMove.CanWalk = false;
    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
        _walkTimer.Tick(deltaTime);
        //Debug.Log(_walkTimer.RemainingSeconds);
    }

    private void EnterAttackState()
    {
        if (Vector2.Distance(_rhino.FindPlayer(), _rhino.transform.position) < 2)
        {
           // _rhino.ChangeState(RhinoState.ShortAttack);
            //return;
        }
        int nextState = 4;
        _rhino.ChangeState((RhinoState)nextState);
    }
}

public class RhinoShortAttack : IState<RhinoBoss>
{
    private BoxCollider2D _trigger;
    private bool _hasBeenEntered;

    public void Enter(RhinoBoss agent)
    {
        Debug.Log("Enter Short Attack State");
        if (!_hasBeenEntered)
        {
            _hasBeenEntered = true;
            _trigger = agent.GetAttackTrigger(1);
        }
    }

    public void Exit(RhinoBoss agent)
    {

    }

    public void Update(RhinoBoss agent, float deltaTime)
    {

    }
}

public class RhinoCharge : IState<RhinoBoss>
{
    private BoxCollider2D _trigger;
    private bool _hasBeenEntered;

    public void Enter(RhinoBoss agent)
    {
        Debug.Log("Enter Charge Attack State");
        if (!_hasBeenEntered)
        {
            _hasBeenEntered = true;
            _trigger = agent.GetAttackTrigger(2);
        }
    }

    public void Exit(RhinoBoss agent)
    {

    }

    public void Update(RhinoBoss agent, float deltaTime)
    {

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