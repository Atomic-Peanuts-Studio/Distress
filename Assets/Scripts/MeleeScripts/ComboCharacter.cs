using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;

    public Collider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        //For Jose
        //Modify for heavy attack, add buttondown logic so it doesn't automatically goto first attack unless you do a mousebuttonup check
        if (Input.GetMouseButton(1))
        {
            Debug.Log(meleeStateMachine.CurrentState);
        }
        if (Input.GetMouseButton(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new SwordEntryState());
        }
    }
}
