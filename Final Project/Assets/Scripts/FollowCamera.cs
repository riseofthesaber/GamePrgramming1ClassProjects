using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FollowCamera : MonoBehaviour
{
    [Tooltip("Offset from the player we'll follow at (based on the camera's position and the player's position)")]
    [SerializeField] private Vector3 offset;

    [Tooltip("Character we are following")]
    [SerializeField] private GameObject following;
    [SerializeField]
    private float LeftBound, RightBound, TopBound;

    private float adjustedRightBound, adjustedLeftBound;

    [SerializeField]
    private bool isLeftBounded = false;
    [SerializeField]
    private bool isRightBounded = false;
    [SerializeField]
    private float followSpeed = 0.00001f;

    [Tooltip("Offset from the player we'll follow at when pushing (based on the camera's position and the player's position)")]
    [SerializeField]
    private float pushingOffsetX;

    public bool pushingLeft = false;
    public bool pushingRight = false;

    private void Awake()
    {
        if (following == null)
        {
            Debug.LogError("You didn't assign something to follow!");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (following == null)
        {
            Debug.LogError("You didn't assign something to follow!");
        }
        transform.position = new Vector3(following.transform.position.x + offset.x,
        following.transform.position.y + offset.y, transform.position.z);
        calculateBounds();
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        float mod = 0;
        if (pushingLeft)
        {
            // if i am pushing left, i want the camera to be further to the right (Positive)
            mod = pushingOffsetX;
        }
        else if (pushingRight)
        {
            // if i am pushing right, i want the camera to be further to the left (Negative)
            mod = -1*pushingOffsetX;
        }

        //transform.position = new Vector3(following.transform.position.x + offset.x+mod, following.transform.position.y + offset.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, 
            new Vector3(following.transform.position.x + offset.x + mod, following.transform.position.y + offset.y, transform.position.z),
            followSpeed * Time.deltaTime);

        //isBounded();
        CheckPositionBounds(adjustedLeftBound, adjustedRightBound);
    }

    //private void isBounded()
    //{
    //    if(this.transform.position.x > adjustedRightBound)
    //    {
    //        isRightBounded = true;
    //    }
    //    else
    //    {
    //        isRightBounded = false;
    //    }
    //}

    public IEnumerator Shake(float shakeFor, float Xforce, float Yforce)
    {
        
        float time = 0.0f;
        while(time<shakeFor){
            float yMod = Yforce* UnityEngine.Random.Range(-1, 1);
            float xMod = Xforce * UnityEngine.Random.Range(-1, 1);

            transform.position = new Vector3(following.transform.position.x + offset.x+ xMod, following.transform.position.y + offset.y+ yMod, transform.position.z);


            time += Time.deltaTime;
            yield return null;
        } 
    }

    private void calculateBounds()
    {
        // assume that these are fields in your class, and camera is a reference to the camera
        // we are pointing at--then adjustedLeftBound is the designer's bound + the half-width
        // of the camera. This makes it easier to just set the position of the camera based on
        // its center.
        adjustedRightBound = RightBound - (GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect);
        adjustedLeftBound = LeftBound + (GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect);
    }
    protected void CheckPositionBounds(float left, float right)
    {
        // take the bigger of these--if the player has moved further
        // left than the left bound (i.e., <) then we want to stick at left
        float leftBound = isLeftBounded ? left : transform.position.x;

        // now take the smaller of these--if the player has moved
        // beyond the right than the right bound (i.e. >) then we want to stick at the right
        float rightBound = isRightBounded ? right : transform.position.x;

        // now clamp the position between these two values
        float bounded = Mathf.Clamp(transform.position.x, leftBound, rightBound);
        transform.position = new Vector3(bounded, transform.position.y, transform.position.z);
        return;
    }
}
