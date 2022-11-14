using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/RandomWait", fileName = "Random Wait Decision")]
public class RandomWaitDecision : Decision
{


    public override bool Decide(StateController controller)
    {
        return (controller.RandomTime <= controller.TimeInState);
    }


}
