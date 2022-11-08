using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    [Header("State Control")]
    [Tooltip("the state that our context is currently in")]
    public State currentState;
    [Tooltip("Whether or not this FSM is active")]
    public bool isActive;

    // generallly shared state goes here

    [Tooltip("The nav mesh agent associated with our entity")]
    public NavMeshAgent navMeshAgent;

    public Transform homeWayPoint;

    public Transform Position;

    public Transform AIeyes;
    // max distance we can see
    public float lookRadius;
    // max range we can see
    public float lookRange;
    // object we are chasing
    public Transform chaseTarget;
    private void Awake()
    {
        Setup();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            currentState.UpdateState(this);
        }
    }

    public void Setup()
    {
        // set up things here
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != currentState)
        {
            OnExitState();

            currentState = nextState;

            currentState.EnterState(this);  
        }
    }

    private void OnExitState()
    {
        currentState.ExitState(this);
    }

}
