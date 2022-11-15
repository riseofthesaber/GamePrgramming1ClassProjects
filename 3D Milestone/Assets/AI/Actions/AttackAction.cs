using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack", fileName = "Attack")]
public class AttackAction : Action
{
    [SerializeField] private LayerMask characterLayerMask;
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        RaycastHit hit;
        if (Physics.SphereCast(controller.AIeyes.position,
        controller.attackRadius, controller.AIeyes.forward, out hit,
        controller.attackRange, characterLayerMask, QueryTriggerInteraction.Ignore))
        {
            GameManager.Lives -= 1;
        }
    }
}


