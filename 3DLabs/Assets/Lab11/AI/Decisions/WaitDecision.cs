using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Wait", fileName = "Wait Decision")]
public class WaitDecision : Decision
{
    [SerializeField] private float WaitForSeconds;
    public override bool Decide(StateController controller)
    {
        return (WaitForSeconds <= controller.TimeInState);
    }


}