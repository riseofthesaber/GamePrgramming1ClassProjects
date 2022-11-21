using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/NearTarget", fileName = "Near Target Decision")]
public class NearDecision : Decision
{
    //[SerializeField] private Transform ThingNear;
    public override bool Decide(StateController controller)
    {
        return ( Vector3.Distance(controller.Target,controller.Position.position)<= controller.NearDistance);
    }


}