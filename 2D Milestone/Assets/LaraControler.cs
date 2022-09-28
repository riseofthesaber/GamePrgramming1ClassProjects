using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Editor;

public class LaraControler : MonoBehaviour
{

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
    private LayerMask wallLayerMask;

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

    [Header("how many wall jumps you get")]
    [SerializeField]
    private float wallJumps = 1 ;

    [Header("multiplies the horizontal jump force added from a wall jump by this value")]
    [SerializeField]
    private float wallJumpXForceMultiplier = 1.5f;

    private float wallJumpsUsed = 0;
    // maximum velocity total
    [SerializeField]
    private float maxVelocityVertical;

    [SerializeField]
    private float maxVelocityHorizontal;

    // slowing factor
    [SerializeField]
    private float frictionAmount;


    // creating a property for maxforce
    public float MoveSpeed { get { return maxMoveSpeed; } set { maxMoveSpeed = value; } }

    private float moveForce;

    private bool isGrounded;

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
    }

    public void MoveActionPreformed(InputAction.CallbackContext context)
    {

        moveInput = new Vector2(context.ReadValue<Vector2>().x, 0);
    }

    private void FixedUpdate()
    {
        Move(moveInput);

        CheckWall(moveInput);

        CheckRunning();

        SpeedCap();
    }

    private void SpeedCap()
    {
        if (Mathf.Abs(body.velocity.x) >= maxVelocityHorizontal)
        {
            body.velocity = new Vector2(maxVelocityHorizontal * Mathf.Sign(body.velocity.x), body.velocity.y);
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
            //calculate the difference between the max and the current
            float difference = maxMoveSpeed-Mathf.Abs(body.velocity.x);
            if (difference > 0) {

                // picks the smallest of either the difference/time (* body mass to account for weight) or the standard move force
                float cap = Mathf.Min(difference / Time.fixedDeltaTime * body.mass, moveForce);
                    body.AddForce(direction * cap, ForceMode2D.Force);
            }
            // if we are moving too fast
            else if (difference < 0)
            {
                body.AddForce(new Vector2(difference * Mathf.Sign(body.velocity.x), 0), ForceMode2D.Impulse);
            }
        }

        else if (isGrounded)
        {
            // slow down fast when on the ground
            float amount = Mathf.Min(Mathf.Abs(body.velocity.x), Mathf.Abs(frictionAmount));

            amount*= Mathf.Sign(body.velocity.x);

            body.AddForce(Vector2.right * -amount * body.mass, ForceMode2D.Impulse);


        }
    }

    private void CheckRunning()
    {
        animator.SetFloat("MoveSpeed", Mathf.Abs(body.velocity.x));
        if (body.velocity.x < -0.001)
        {
            SpriteRenderer.flipX = true;
        }
        else if (body.velocity.x > 0.001)
        {
            SpriteRenderer.flipX = false;
        }
    }

    private void CheckGrounded()
    {
        RaycastHit2D GroundCast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, environmentLayerMask);

        isGrounded = (GroundCast.collider != null);
        //Debug.Log("ground");
        animator.SetBool("Grounded", isGrounded);
        if(isGrounded) wallJumpsUsed = 0;
    }

    private void CheckWall(Vector2 direction)
    {
        // only check if the direction is not zero and there are wall jumps left to use
        if ( wallJumps>wallJumpsUsed && !Mathf.Approximately(direction.x, 0))
        {
            RaycastHit2D boxCastHit;
            if (SpriteRenderer.flipX)
            {
                boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left, .1f, wallLayerMask);
            }
            else
            {
                boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right, .1f, wallLayerMask);
            }
            onWall = (boxCastHit.collider != null);
            if (onWall) Debug.Log("Wall");
            //animator.SetBool("onWall", onWall);
        }
        else
        {
            onWall = false;
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
            }
            else if (onWall)
            {
                //Debug.Log("WallJump");
                if (SpriteRenderer.flipX)
                {
                    body.AddForce((Vector2.up + (Vector2.right * wallJumpXForceMultiplier)) * jumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    body.AddForce((Vector2.up + (Vector2.left * wallJumpXForceMultiplier)) * jumpForce, ForceMode2D.Impulse);
                }
                animator.SetTrigger("Jump");
                wallJumpsUsed++;
            }

        }
        else if (context.canceled)
        {// if she is jumping
            //Debug.Log("test");
            if (body.velocity.y > 0)
            {
                // cancle the jump force by half
                body.AddForce(Vector2.down * body.velocity.y * .5f * body.mass, ForceMode2D.Impulse);
            }
        }
    }
}
