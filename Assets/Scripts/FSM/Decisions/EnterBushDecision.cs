using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Enter Bush")]
public class EnterBushDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        var enemyInLineOfSight = stateMachine.GetComponent<EnemySightSensor>();
        var isInsideBush = stateMachine.GetComponent<IsInBush>();

        return enemyInLineOfSight.Player.GetComponent<PlayerController>().isHiddenBush;
    }
}
