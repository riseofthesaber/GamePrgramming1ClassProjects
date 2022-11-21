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

    public float NearDistance;
    
    public Transform homeWayPoint;

    public Transform Position;

    public Transform AIeyes;
    // max distance we can see
    public float lookRadius;
    // max range we can see
    public float lookRange;
    // object we are chasing
    public Transform chaseTarget;

    public AIStats aiStats;

    public float attackRadius;
    // max range we can see
    public float attackRange;
    // object we are chasing

    // value that keeps track of the time
    public float TimeInState = 0;
    public Vector3 Target;

    [SerializeField] private Color LookGizmoColor;

    [SerializeField] private Color AttackGizmoColor;

    [Header("Animator")]
    [Tooltip("The animator used by this game object")]
    [SerializeField]
    public Animator animator;

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
            TimeInState += Time.deltaTime;
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

            OnEnterState();  
        }
    }

    private void OnExitState()
    {
        currentState.ExitState(this);
    }

    private void OnEnterState()
    {
        TimeInState = 0;
        currentState.EnterState(this);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = LookGizmoColor;

        Gizmos.DrawWireSphere(AIeyes.transform.position + AIeyes.transform.forward * lookRange, lookRadius);

        Gizmos.color = AttackGizmoColor;

        Gizmos.DrawWireSphere(AIeyes.transform.position + AIeyes.transform.forward * attackRange, attackRadius);
    }

}
