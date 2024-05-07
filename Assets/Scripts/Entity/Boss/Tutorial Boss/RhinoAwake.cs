using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoAwake : IState<RhinoBoss>
{
    private RhinoBoss _agent;

    private bool _doorsClosed;

    private float _animationTime;
    private Timer _animationTimer;

    public void Enter(RhinoBoss agent)
    {
        Debug.Log("rawr");
        _agent = agent;

        //get animation time
        _animationTime = 3f;

        _animationTimer = new Timer(_animationTime);
        _animationTimer.OnTimerEnd += ChangeState;

        Player.PauseMovement();
    }

    public void Exit(RhinoBoss agent)
    {
        agent.ShowHealthBar();

        Player.UnpauseMovement();
    }

    public void Update(RhinoBoss agent, float deltaTime)
    {
        //close doors halfway through animation
        _animationTimer.Tick(deltaTime);

        if (_animationTimer.RemainingSeconds <= _animationTime * 0.5f && !_doorsClosed)
        {
            agent.CloseDoors();
            _doorsClosed = true;
        }
    }

    private void ChangeState()
    {
        _agent.ChangeState(RhinoState.Walk);
    }
}
