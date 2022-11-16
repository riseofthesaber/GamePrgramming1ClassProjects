using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/GoHome", fileName = "GoHome")]
public class GoHomeAction : Action
{
    public override void Act(StateController controller)
    {
        GoHome(controller);
    }

    private void GoHome(StateController controller)
    {
        controller.navMeshAgent.destination = controller.homeWayPoint.position;


        controller.navMeshAgent.isStopped = false;  
    }
}
