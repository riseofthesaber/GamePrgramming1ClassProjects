using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EllenControl : MonoBehaviour
{
    [Tooltip("The animator used by this game object")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private LayerMask environmentLayerMask;

    [SerializeField]
    private LayerMask pushingLayerMask;

    private Vector2 moveInput;

    // the character's movement speed
    [SerializeField]
    private float maxMoveSpeed;

    // how fast the character accelerates
    [SerializeField]
    private float moveAcceleration;

    // how much force from jumping
    [SerializeField]
    private float jumpForce;

    // maximum velocity total
    [SerializeField]
    private float maxVelocityVertical;

    [SerializeField]
    private float maxVelocityHorizontal;

    // slowing factor
    [SerializeField]
    private float frictionAmount;

    [SerializeField]
    private Transform CharacterTransform;


    // creating a property for maxforce
    public float MoveSpeed { get { return maxMoveSpeed; } set { maxMoveSpeed = value; } }

    private float moveForce;

    private bool isGrounded;

    private bool isPushing;

    private bool onWall;

    private void Awake()
    {
        if (animator == null || body == null)
        {
            Debug.LogError("you forgot the animator or the body");
        }
        moveForce = body.mass * moveAcceleration;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        CheckPushing(moveInput);
        CheckWall(moveInput);
    }
    private void OnValidate()
    {
        // so if you change while gane is running for good testing stuff
        moveForce = body.mass * moveAcceleration;
    }
    public void MoveActionPreformed(InputAction.CallbackContext context)
    {

        moveInput = new Vector2(context.ReadValue<Vector2>().x, 0);
    }

    private void FixedUpdate()
    {
        Move(moveInput);

        //CheckGround();

        CheckRunning();

        SpeedCap();

        //      if (Mathf.Abs(body.velocity.x) >= maxVelocityHorizontal)
        //    {
        //      body.velocity = new Vector2(maxVelocityHorizontal, body.velocity.y);
        //}
        //      if (Mathf.Abs(body.velocity.y) >= maxVelocityVertical)
        //    {
        //      body.velocity = new Vector2(body.velocity.x, maxVelocityVertical);
        //}
    }

    private void SpeedCap() {
              if (Mathf.Abs(body.velocity.x) >= maxVelocityHorizontal)
            {
              body.velocity = new Vector2(maxVelocityHorizontal*Mathf.Sign(body.velocity.x), body.velocity.y);
        }
              if (Mathf.Abs(body.velocity.y) >= maxVelocityVertical)
            {
              body.velocity = new Vector2(body.velocity.x, maxVelocityVertical * Mathf.Sign(body.velocity.y));
        }
    }

    private void Move(Vector2 direction)
    {
        // only move if the direction is not zero
        if (!Mathf.Approximately(direction.x, 0))
        {
            float speedDiff = maxMoveSpeed - Mathf.Abs(body.velocity.x);

            if (!Mathf.Approximately(speedDiff, 0))
            {
                if (speedDiff > 0)
                {
                    // picks the smaller of the two
                    float accelCap = Mathf.Min(speedDiff / Time.fixedDeltaTime * body.mass, moveForce);
                    body.AddForce(direction * accelCap, ForceMode2D.Force);
                }
                // if we are moving too fast
                else if (speedDiff < 0)
                {
                    body.AddForce(new Vector2(speedDiff * Mathf.Sign(body.velocity.x), 0), ForceMode2D.Impulse);
                }
            }
        }

        else if (isGrounded)
        {
            //get amount
            float amount = Mathf.Min(Mathf.Abs(body.velocity.x), Mathf.Abs(frictionAmount));

            //get the movement direction
            amount *= Mathf.Sign(body.velocity.x);

            //apply a braking impulse to the player's velocity
            body.AddForce(Vector2.right * -amount * body.mass, ForceMode2D.Impulse);
        }

    }

    private void CheckRunning()
    {
        animator.SetFloat("MoveSpeed", Mathf.Abs(body.velocity.x));
        if (body.velocity.x < -0.001)
        {
            lookLeft();
        }
        else if (body.velocity.x > 0.001)
        {
            //SpriteRenderer.flipX = false;
            lookRight();
        }
    }

    private void lookLeft() {
        // thanks to u/ThrownShield on reddit who proposed this idea for an alternative to using flipX that also flips the hitbox
        //this code to execute the idea is entirely oiginal but the idea of using transform was not mine
        if (CharacterTransform.localScale.x > 0) CharacterTransform.localScale = new Vector3(-1f , 1, 1);
    }

    private void lookRight()
    {
        if (CharacterTransform.localScale.x < 0) CharacterTransform.localScale = new Vector3(1f, 1, 1);

    }

    private void CheckGrounded()
    {
        RaycastHit2D boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, environmentLayerMask);

        isGrounded = (boxCastHit.collider != null);
        //Debug.Log("ground");
        animator.SetBool("Grounded", isGrounded);

    }

    private void CheckPushing(Vector2 direction)
    {
        // only move if the direction is not zero
        if (!Mathf.Approximately(direction.x, 0))
        {
            RaycastHit2D boxCastHit;
            if (CharacterTransform.localScale.x < 0)
            {
                boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left, .1f, pushingLayerMask);
            }
            else
            {
                boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right, .1f, pushingLayerMask);
            }
            isPushing = (boxCastHit.collider != null);
            //if (isPushing) Debug.Log("push");
            animator.SetBool("Pushing", isPushing);
        }
        else {
            //dont push 
            animator.SetBool("Pushing", false);
        }

    }

    private void CheckWall(Vector2 direction)
    {
        // only check if the direction is not zero
        if (!Mathf.Approximately(direction.x, 0))
        {
            RaycastHit2D boxCastHit;
            if (CharacterTransform.localScale.x < 0)
            {
                boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left, .1f, environmentLayerMask);
            }
            else
            {
                boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right, .1f, environmentLayerMask);
            }
            onWall = (boxCastHit.collider != null);
            //if (onWall) Debug.Log("Wall");
            //animator.SetBool("onWall", onWall);
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                animator.SetTrigger("Jump");
            }else if (onWall) {
                //Debug.Log("WallJump");
                if (CharacterTransform.localScale.x < 0)
                {
                    body.AddForce((Vector2.up+ (Vector2.right*1.5f)) * jumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    body.AddForce((Vector2.up + (Vector2.left*1.5f)) * jumpForce, ForceMode2D.Impulse);
                }
                animator.SetTrigger("Jump");
            }

        }
        else if (context.canceled)
        {// if she is jumping
           // Debug.Log("test");
            if (body.velocity.y > 0)
            {
                // cancle the jump force by half
                body.AddForce(Vector2.down * body.velocity.y * .5f * body.mass, ForceMode2D.Impulse);
            }
        }

    }

}
