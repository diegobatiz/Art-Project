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

public class RhinoBoss : MonoBehaviour
{
    private StateMachine<RhinoBoss> _stateMachine;
    private RhinoState _currentState;

    [SerializeField] private Door _leftDoor;
    [SerializeField] private Door _rightDoor;


    private void Awake()
    {
        _currentState = RhinoState.None;

        _stateMachine = new StateMachine<RhinoBoss>(this);
        _stateMachine.AddState<RhinoAwake>();
        _stateMachine.AddState<RhinoWalk>();
        _stateMachine.AddState<RhinoShortAttack>();
        _stateMachine.AddState<RhinoCharge>();
        _stateMachine.AddState<RhinoShockwave>();

        ChangeState(RhinoState.Awake);
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

    public void ShowHealthBar()
    {
        Debug.Log("poof, healthbar appear");
    }

    public void CloseDoors()
    {
        _leftDoor.Close();
        _rightDoor.Close();
    }
}
