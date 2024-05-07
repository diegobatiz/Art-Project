using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RhinoState
{
    None,
    Awake,
    Walk,
    ShortAttack,
    ChargeAttack,
    Jump,
    Dying
};

public class RhinoBoss : MonoBehaviour
{
    private StateMachine<RhinoBoss> _stateMachine;
    private RhinoState _currentState;

    private void Awake()
    {
        _stateMachine = new StateMachine<RhinoBoss>(this);
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
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
}
