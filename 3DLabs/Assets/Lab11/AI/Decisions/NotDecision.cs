using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Not", fileName = "Not Decision")]
public class NotDecision : Decision
{
    [SerializeField] private Decision InputDecision;
    public override bool Decide(StateController controller)
    {
        return! InputDecision;
    }


}
