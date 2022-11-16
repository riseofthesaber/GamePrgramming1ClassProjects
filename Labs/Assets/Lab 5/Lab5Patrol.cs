using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab5Patrol : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("enemyMovement")]
    [SerializeField] private float moveSpeed = 0.8f;
    [SerializeField] private float waitTime = 2f;

    [SerializeField] private bool enemyPatrolling = true;

    [SerializeField]
    private List<Vector2> locations = new List<Vector2>() { new Vector2(3, 1.5f), new Vector2(-3, -1.5f), new Vector2(0, -1.5f), new Vector2(0, 3f) };
    [SerializeField] private int TargetLocation = 1;
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Move()
    {
        Vector2 targetPosition = transform.position;
        Vector2 directionVec = (locations[TargetLocation] - new Vector2(transform.position.x, transform.position.y)).normalized;
        targetPosition += directionVec * moveSpeed * Time.fixedDeltaTime;
        enemyRigidbody.MovePosition(targetPosition);
        if (Mathf.Sign(directionVec.x) == -1)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void CheckMoveDirection()
    {
  
    }

    private IEnumerator EnemyPatrol()
    {
        while (enemyPatrolling)
        {
            //while ((!Mathf.Approximately(transform.position.x, locations[TargetLocation].x))| (!Mathf.Approximately(transform.position.y, locations[TargetLocation].y)))
            while (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), (locations[TargetLocation])) > 0.01f)
            {
                //Debug.Log("Move");

                Move();

                yield return new WaitForFixedUpdate();
            }

            //if (TargetLocation >= locations.Count) {
            //    TargetLocation = 0;
            //}
            //else
            //{
            //    TargetLocation+=1;
            //    Debug.Log("change");
            //}

            TargetLocation += 1;
            TargetLocation %= locations.Count;
            float counter = 0f;
            while (counter < waitTime)
            {
                //Debug.Log("wait");
                counter += Time.deltaTime;
                yield return null;
            }
        }


    }

    void Start()
    {
        Debug.Log("start");
        StartCoroutine(EnemyPatrol());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
