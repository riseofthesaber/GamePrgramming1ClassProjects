using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/TriggerAnimation", fileName = "TriggerAnimation")]
public class TriggerAnimation : Action
{
    [Header("Trigger Name")]
    [SerializeField] private string TriggerName;
   public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        //Debug.Log(TriggerName);
        controller.animator.SetTrigger(TriggerName);

    }
}