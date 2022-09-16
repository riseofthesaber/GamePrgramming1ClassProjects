using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back_and_forth : MonoBehaviour
{



    [SerializeField]
    private float maxX =  0.4f;

    [SerializeField]
    private float minX = -2.6f;

    [SerializeField]
    private Vector2 velocity = new Vector2(0.75f, 1.1f);


    private bool up = false;
    private Rigidbody2D Bod;

    // Start is called before the first frame update
    void Start()
    {
        Bod = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (up && Bod.position.x > maxX ) { 
            up = false;
        }
        if ((!up) && Bod.position.x < minX) { 
            up = true;
        }
        if (up)
        {
            Bod.MovePosition(Bod.position + velocity * Time.fixedDeltaTime);
        }else
        {
            Bod.MovePosition(Bod.position - velocity * Time.fixedDeltaTime);
        }

    }
}
