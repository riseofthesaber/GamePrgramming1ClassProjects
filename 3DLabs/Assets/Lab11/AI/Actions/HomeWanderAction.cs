using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/HomeWander", fileName = "Home Wander Action")]
public class HomeWanderAction : Action
{
    public override void Act(StateController controller)
    {
        NavigateToRandomPoint(controller);
    }

    private void NavigateToRandomPoint(StateController controller)
    {
        // Find a random point wihtin maxDistanceFromHome distance of the home waypoint
        Vector3 randomPointInWanderRadius = controller.homeWayPoint.position + (Random.insideUnitSphere * controller.aiStats.maxDistanceFromHome);
        NavMeshHit hit;

        // Sample position takes a point and projects it vertically onto the NavMesh to find the nearest point it hits
        // the output of which goes into the "hit" variable
        NavMesh.SamplePosition(randomPointInWanderRadius, out hit, controller.aiStats.maxDistanceFromHome, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;

        // Set the navigation destination and move towards it
        controller.navMeshAgent.destination = finalPosition;
        controller.navMeshAgent.isStopped = false;

        Debug.Log("HomeWander agent set new destination at " + finalPosition);
    }
}
