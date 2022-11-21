using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.position = new Vector3(following.transform.position.x + offset.x,
            following.transform.position.y + offset.y, transform.position.z);
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
