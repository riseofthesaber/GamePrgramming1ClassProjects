using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMove : BaseMove
{

    // acceleration speed
    [SerializeField] private float moveAcceleration = 60f;


    // current max speed
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 70f;

    [SerializeField] private Vector3 cameraAdjustedInputDirection;

    [SerializeField] private Rigidbody characterRigidbody;

    [SerializeField] private float rotationSpeed;


    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Tooltip("which layers are considered ground")]
    [SerializeField] private LayerMask environmentLayerMask;

    [SerializeField] private bool isGrounded;

    [Header("max fall speed")]
    [SerializeField] private float maxVerticalMoveSpeed = 25f;

    [Header("Object Refrences")]
    [SerializeField] private CapsuleCollider capsuleCollider;

    [SerializeField] private Transform characterModel;

    private bool wasGroundedLastFrame;
    [Header("Character Animator")]
    [Tooltip("The animator used by this game object")]
    [SerializeField]
    private Animator animator;
    private bool IsJumping;


    // check if grounded
    private void CheckGround()
    {
        // store last frame
        wasGroundedLastFrame = isGrounded;

        // use an overlappig sphere
        Vector3 origin = transform.position + (Vector3.up * (capsuleCollider.radius - groundCheckDistance));

        // query the physics engine for collisions

        Collider[] overlappedColliders = Physics.OverlapSphere(origin, (capsuleCollider.radius * 0.95f), environmentLayerMask, QueryTriggerInteraction.Ignore);

        // determine if grounded
        isGrounded = (overlappedColliders.Length > 0);

        //Do stuff when we reach the ground, only once
        if(!wasGroundedLastFrame && isGrounded)
        {
            IsJumping = false;   
        }

        // set our animator parameter here!!!!!
        animator.SetBool("IsGrounded", isGrounded);

    }

    private void CheckFalling()
    {
        animator.SetBool("IsFalling", (characterRigidbody.velocity.y < 0));


        //if (characterRigidbody.velocity.y < 0) {
        //    animator.SetBool("IsFalling", true);
        //}
        //else
        //{
        //    animator.SetBool("IsFalling", false);
        //}

    }
    //private void checkRunning
    private void CheckSpeed()
    {
        Vector3 currentVelocity = GetHorizontalRBVelocity();

        animator.SetFloat("MoveSpeed", currentVelocity.sqrMagnitude);


    }

    private void MoveCharacter()
    {
        if (cameraAdjustedInputDirection != Vector3.zero)
        {
            characterRigidbody.AddForce(cameraAdjustedInputDirection * moveAcceleration * characterRigidbody.mass, ForceMode.Force);
        }
    }

    private float GetMaxAllowedVelocity()
    {
        return moveSpeed * cameraAdjustedInputDirection.magnitude;
    }

    private Vector3 GetHorizontalRBVelocity()
    {

        return new Vector3(characterRigidbody.velocity.x, 0, characterRigidbody.velocity.z);

    }

    private void LimitVelocity()
    {
        Vector3 currentVelocity = GetHorizontalRBVelocity();
        float maxAllowedVelocity = GetMaxAllowedVelocity();

        if (currentVelocity.sqrMagnitude > (maxAllowedVelocity * maxAllowedVelocity))
        {
            // a squared +b squared = c squared, we are comparing the c squared of the max and the current


            Vector3 counteractDirection = currentVelocity.normalized * -1f;

            float counteractAmount = currentVelocity.magnitude - maxAllowedVelocity;

            // applies a horizontal force to slow them down to the max speed
            characterRigidbody.AddForce(counteractDirection * counteractAmount * characterRigidbody.mass, ForceMode.Impulse);
        }

        if (!isGrounded)
        {
            // vertical fall and jump velocity cap
            if (Mathf.Abs(characterRigidbody.velocity.y) > maxVerticalMoveSpeed)
            {

                Vector3 counteractDirection = Vector3.up * Mathf.Sign(characterRigidbody.velocity.y) * -1f;

                float counteractAmount = Mathf.Abs(characterRigidbody.velocity.y) - maxVerticalMoveSpeed;
                characterRigidbody.AddForce(counteractDirection * counteractAmount * characterRigidbody.mass, ForceMode.Impulse);
            }

        }
    }
    /*    private void UpStairsAndSlopes() {
            //if i could come up with some way of detecting what height a change in elevation occurs, or at last how far away the change occurs i could calculate how high i need to move the character up
            // how can i do this withoug having hundeds of rays?
            // do i just have one ray where i cast it from  predetermined height, how far would i cast it?

            // maybe 2 rays, one to detect if there is a collision, and the other to detect if the collision is too steep 
            // if the second one isint hit then we move the character up by the amount
            float maxSlopeAngle = 30;

            // assuming that the hypotenuse length is 1 calculate the height and distance of the second ray
            float maxSlopeHeight = Mathf.Sin(maxSlopeAngle);
            float maxSlopeDistance = Mathf.Cos(maxSlopeAngle);

            // now i need to shrink the rays by a certian size to work on a small scale

            // origin of ray 1 starts a the base but .1 up so we dont hit the floor
            Vector3 origin1 = transform.position + (Vector3.up * 0.1f);
            //origin1 of ray2 starts at the calculated maximum slope height
            Vector3 origin2 = transform.position + (Vector3.up * maxSlopeHeight);

    *//*        Collider[] overlappedColliders = Physics.overlap(origin, (capsuleCollider.radius * 0.95f), environmentLayerMask, QueryTriggerInteraction.Ignore);

            isGrounded = (overlappedColliders.Length > 0);*//*
        }*/



    override public void Move(Vector3 moveDir)
    {
        //implament movement 
        cameraAdjustedInputDirection = moveDir;


    }

    override public void RotateCharacter()
    {
        if (cameraAdjustedInputDirection != Vector3.zero)
        {

            // characterModel should be a reference to the actual 3D model
            characterModel.forward = Vector3.Slerp(characterModel.forward, cameraAdjustedInputDirection.normalized, Time.deltaTime * rotationSpeed);

        }
    }
    override public void Jump()
    {
        //Debug.Log("please");
        if (isGrounded && !IsJumping)
        {
           // Debug.Log("jump");
            characterRigidbody.AddForce(Vector3.up * jumpForce * characterRigidbody.mass, ForceMode.Impulse);
            // add animator here
            animator.SetTrigger("Jump");
            IsJumping = true;
        }
    }
    override public void JumpCanceled()
    {
        if (characterRigidbody.velocity.y > 0)
        {
            // cancle the jump force by half
            characterRigidbody.AddForce(Vector3.down * characterRigidbody.velocity.y * .5f * characterRigidbody.mass, ForceMode.Impulse);
        }
    }



    override public void Dance()
    {
        Vector3 currentVelocity = GetHorizontalRBVelocity();
        if (currentVelocity.sqrMagnitude == 0)
        {
            animator.SetTrigger("Dance");
        }

    }


    // Start is called before the first frame update
    override protected void Start()
    {

    }

    // Update is called once per frame
    override protected void Update()
    {

    }

    override protected void FixedUpdate()
    {
        //check grounding
        CheckGround();
        CheckFalling();
        CheckSpeed();

        MoveCharacter();

        LimitVelocity();
        RotateCharacter();

    }

}
