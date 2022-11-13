using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="PluggableAI/Decisions/look", fileName ="Look Decision")]
public class LookDecision : Decision
{
    [SerializeField] private LayerMask characterLayerMask;
    public override bool Decide(StateController controller)
    {
        bool TargetVisible = Look(controller);
        return TargetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;
        //Collider[] cols;

        if (Physics.SphereCast(controller.AIeyes.position, 
            controller.lookRadius, controller.AIeyes.forward,out hit,
            controller.lookRange, characterLayerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log("i see you");
            controller.chaseTarget = hit.transform;
            return true;
        }
        else {
            return false;
        }
       //return false;
    }
}
