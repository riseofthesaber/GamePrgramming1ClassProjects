using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Selector", fileName = "Selector Decision")]
public class SelectorDecision : Decision
{
    [SerializeField] private List<Decision> InputDecisions;
    public override bool Decide(StateController controller)
    {
        // if any one of them is true return true, otherwise return false
        foreach (Decision decision in InputDecisions)
        {
            if (decision.Decide(controller))
            {
                return true;
            }
        }
        return false;
    }


}