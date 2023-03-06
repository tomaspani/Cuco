using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/EnterBush")]
public class EnterBushAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();
        var patrolPoints = stateMachine.GetComponent<PatrolPoints>();
        patrolPoints.SetBool(false);
        navMeshAgent.SetDestination(enemySightSensor.Player.position - new Vector3(0,0,-1));
    }
}
