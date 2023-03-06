using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Is Inside Bush")]
public class IsInsideBush : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var isInBush = stateMachine.GetComponent<IsInBush>();
        return isInBush.getInBush();
    }
}
