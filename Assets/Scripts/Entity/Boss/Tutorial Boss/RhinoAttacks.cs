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
            _walkTimer = new Timer(_stateTime);
            _hasBeenEntered = true;
        }

        //SET THE WALK DIRECTION TO EITHER 1 or -1

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