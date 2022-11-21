using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Hyena_Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;

    [SerializeField] private Rigidbody2D enemyRigidbody;

    [SerializeField] private LayerMask environmentLayerMask;

    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] private SpriteRenderer SpriteRenderer;

    private bool onEdge;
    private bool onWall;
    private bool isGrounded;
    private enum Direction
    {
        Right, Left
    }

    private Direction moveDirection;



    private void Move()
    {
        Vector2 targetPosition = transform.position;
        switch (moveDirection)
        {
            default:
            case Direction.Right:
                targetPosition += Vector2.right * moveSpeed * Time.fixedDeltaTime;
                break;
            case Direction.Left:
                targetPosition -= Vector2.right * moveSpeed * Time.fixedDeltaTime;
                break;

        }
        /// MOVE POSITION HYENA
        enemyRigidbody.MovePosition(targetPosition);
    }


    private void switchMoveDirection()
    {

        if (moveDirection == Direction.Right)
        {
            moveDirection = Direction.Left;
            SpriteRenderer.flipX = false;

        }
        else
        {
            moveDirection = Direction.Right;
            SpriteRenderer.flipX = true;
        }

    }


    private void CheckFloor()
    {
        // checks if the hyena is grounded and if the ground in front of it is still there

        //grounding
        RaycastHit2D GroundCast = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, environmentLayerMask);

        isGrounded = (GroundCast.collider != null);

        //edge check
        RaycastHit2D rayCastEdgeHit;
        RaycastHit2D rayCastWallHit;
        if (moveDirection == Direction.Left)
        {
            rayCastWallHit = Physics2D.Raycast(new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.size.y), Vector2.left, 0.1f, environmentLayerMask);
            rayCastEdgeHit = Physics2D.Raycast(new Vector2(boxCollider2D.bounds.min.x, boxCollider2D.bounds.min.y), Vector2.down, 0.1f, environmentLayerMask);
            //(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, (Vector2.left + (Vector2.down/10)), boxCollider2D.bounds.size.x, environmentLayerMask);
        }
        else
        {
            rayCastWallHit = Physics2D.Raycast(new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.size.y), Vector2.right, 0.1f, environmentLayerMask);
            rayCastEdgeHit = Physics2D.Raycast(new Vector2(boxCollider2D.bounds.max.x, boxCollider2D.bounds.min.y), Vector2.down, 0.1f, environmentLayerMask);

            //boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, (Vector2.right + (Vector2.down / 10)), boxCollider2D.bounds.size.x, environmentLayerMask);
        }
        // returns true if there is nothing in front of it
        onEdge = !(rayCastEdgeHit.collider != null);
        onWall = (rayCastWallHit.collider != null);
        if (onEdge)
        {
            //Debug.Log("Edge");
       
                switchMoveDirection();

        }
        else
        {
            onEdge = false;
        }

        if (onWall)
        {
            switchMoveDirection();
        }
        else
        {
            onWall = false;
        }

        if (!isGrounded)
        {
            //Debug.Log("NoGround");
        }
        else
        {
            //Debug.Log("Ground");
        }
    }








    private void FixedUpdate()
    {
        if ((!onEdge) && isGrounded)
        {
            Move();
        }


        CheckFloor();
    }
    // Update is called once per frame
    void Update()
    {

    }

}
