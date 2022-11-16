using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Sequence", fileName = "Sequence Decision")]
public class SequenceDecision : Decision
{
    [SerializeField] private List<Decision> InputDecisions;
    public override bool Decide(StateController controller)
    {
        // if any one of them is false return false, otherwise return true
        foreach(Decision decision in InputDecisions)
        {
            if (!decision.Decide(controller))
            {
                return false;
            }
        }
        return true;
    }


}
