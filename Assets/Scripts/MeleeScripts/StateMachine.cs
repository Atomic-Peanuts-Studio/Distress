using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //Singleton
    public string weaponName="Attack";
    public static StateMachine Instance { get; private set; }
    private void Awake()
    {
        //If there is an instance, and it's not me, delete myself
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        SetNextStateToMain();
    }

    private State mainStateType = new IdleCombatState();

    public State CurrentState { get; private set; }
    private State nextState;

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
        }

        if (CurrentState != null)
            CurrentState.OnUpdate();
    }

    public State GetCurrentState()
    {
        return CurrentState;
    }

    private void SetState(State _newState)
    {
        nextState = null;
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    private void LateUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnLateUpdate();
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }

    private void OnValidate()
    {
 
      
    }
}
