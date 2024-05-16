using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState<AgentType> where AgentType : class
{
    public void Enter(AgentType agent);
    public void Update(AgentType agent, float deltaTime);
    public void Exit(AgentType agent);
}

public class StateMachine<AgentType> where AgentType : class
{
    protected AgentType _agent;
    protected IState<AgentType> _currentState = null;
    protected IState<AgentType> _lastState = null;
    protected List<IState<AgentType>> _states;
    protected int _stateIndex = -1;
    public int CurrentState { get { return _stateIndex; } }

    public StateMachine(AgentType agent)
    {
        _agent = agent;
        _states = new List<IState<AgentType>>();
    }

    public void AddState<StateType>() where StateType : IState<AgentType>, new()
    {
        _states.Add(new StateType());
    }

    public int GetLastState()
    {
        for (int i = 0; i < _states.Count; i++)
        {
            if (_lastState == _states[i])
            {
                return i;
            }
        }
        return -1;
    }

    public void Update(float deltaTime)
    {
        _currentState.Update(_agent, deltaTime);
    }

    public void ChangeState(int index)
    {
        if (_currentState != null)
        {
            _currentState.Exit(_agent);
            _lastState = _currentState;
        }

        _currentState = _states[index];
        _currentState.Enter(_agent);
        _stateIndex = index;
    }
}
