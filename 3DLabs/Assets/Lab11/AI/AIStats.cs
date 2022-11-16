using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Stats", fileName = "DefaultStats")]
public class AIStats : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("The maximium distance this agent can be from the home waypoint for home distance Decisions")]
    public float maxDistanceFromHome = 10f;

    [Header("Perception")]
    [Tooltip("The length of the SphereCast this agent uses in Look Decisions")]
    public float lookRange = 8f;
    [Tooltip("The radius of the SphereCast this agent uses in Look Decisions")]
    public float lookSphereCastRadius = 1f;

    [Header("Attacking")]
    [Tooltip("The range within which the player must be for the AI to be able to attack")]
    public float attackRange = 2f;
    [Tooltip("The amount of time in seconds between each AI attack")]
    public float attackRate = 1f;

    [Header("Searching")]
    [Tooltip("The amount of time in seconds this AI will search for in Scan States")]
    public float searchDuration = 8f;
    [Tooltip("The speed at which this AI will turn in Scan States")]
    public float searchingTurnSpeed = 120f;
}
