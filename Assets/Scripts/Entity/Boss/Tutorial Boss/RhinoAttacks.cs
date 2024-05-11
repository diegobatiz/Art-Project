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
            _walkTimer.OnTimerEnd += EnterAttackState;
            _maxTime = _rhino.MaxWalkTime;
            _minTime = _rhino.MinWalkTime;

            _hasBeenEntered = true;
        }

        int direction = _rhino.FindPlayer().x > _rhino.transform.position.x ? 1 : -1;
        _rhinoMove.SetDirection(direction);

        _stateTime = Random.Range(_minTime, _maxTime);
        _walkTimer = new Timer(_stateTime);
        _rhinoMove.CanWalk = true;
    }

    public void Exit(RhinoBoss agent)
    {
        _rhinoMove.CanWalk = false;
    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
       
    }

    private void EnterAttackState()
    {
        if (Vector2.Distance(_rhino.FindPlayer(), _rhino.transform.position) > 3)
        {
            _rhino.ChangeState(RhinoState.ShortAttack);
            return;
        }
        int nextState = Random.Range(2, 4);
        _rhino.ChangeState((RhinoState)nextState);
    }
}

public class RhinoShortAttack : IState<RhinoBoss>
{
    public void Enter(RhinoBoss agent)
    {

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
    public void Enter(RhinoBoss agent)
    {

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
    public void Enter(RhinoBoss agent)
    {

    }

    public void Exit(RhinoBoss agent)
    {

    }

    public void Update(RhinoBoss agent, float deltaTime)
    {

    }
}