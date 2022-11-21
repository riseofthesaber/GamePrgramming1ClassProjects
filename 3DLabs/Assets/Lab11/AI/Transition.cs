using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition 
{
    [Tooltip("The decision being evaluated b this transition")]
    public Decision decision;

    [Tooltip("the next tate to transition to")]
    public State nextState;

}
