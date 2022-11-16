using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/ChasePlayer", fileName = "ChasePlayer")]
public class ChasePlayer : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.navMeshAgent.destination = controller.chaseTarget.position;


        controller.navMeshAgent.isStopped = false;
    }
}
