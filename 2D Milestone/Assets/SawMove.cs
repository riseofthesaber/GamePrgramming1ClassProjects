using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SawMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float moveTime = 2f;

    [SerializeField] private bool sawMoving = true;

    [SerializeField] private Rigidbody2D enemyRigidbody;

    private enum Direction
    {
        Up, Down
    }
    private Direction moveDirection;

    private void Move()
    {
        Vector2 targetPosition = transform.position;
        switch (moveDirection)
        {
            default:
            case Direction.Up:
                targetPosition += Vector2.up * moveSpeed * Time.fixedDeltaTime;
                break;
            case Direction.Down:
                targetPosition -= Vector2.up * moveSpeed * Time.fixedDeltaTime;
                break;
        }

        enemyRigidbody.MovePosition(targetPosition);
    }

    private void switchMoveDirection()
    {

        if (moveDirection == Direction.Up)
        {
            moveDirection = Direction.Down;

        }
        else
        {
            moveDirection = Direction.Up;
        }
    }

    private IEnumerator sawMove()
    {
        while (sawMoving)
        {
            float counter = 0f;
            while (counter < moveTime)
            {
                counter += Time.fixedDeltaTime;

                Move();

                yield return new WaitForFixedUpdate();
                //Debug.Log(counter);
            }
            //Debug.Log("switch");
            switchMoveDirection();
        }
    }


    void Start()
    {
        StartCoroutine(sawMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
