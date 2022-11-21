using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(menuName ="PluggableAI/State", fileName = "State")]
public class State : ScriptableObject
{
    [SerializeField] private List<Action> enterActions;
    [SerializeField] private List<Action> exitActions;
    [SerializeField] private List<Action> updateActions;

    [SerializeField] private List<Transition> transitions;

    public void EnterState (StateController controller)
    {
        DoEnterActions(controller);
    }
    public void ExitState(StateController controller)
    {
        DoExitActions(controller);
    }
    public void UpdateState(StateController controller)
    {
        DoUpdateActions(controller);

        CheckTransitions(controller);
    }

    private void DoEnterActions(StateController controller)
    {
        foreach(Action enterAction in enterActions)
        {
            enterAction.Act(controller);
        }
    }

    private void DoExitActions(StateController controller)
    {
        foreach (Action exitAction in exitActions)
        {
            exitAction.Act(controller);
        }
    }

    private void DoUpdateActions(StateController controller)
    {
        foreach (Action updateAction in updateActions)
        {
            updateAction.Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transition.nextState);
                break;       
            }
        }
    }

}
