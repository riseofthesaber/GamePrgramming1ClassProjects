using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private float JumpForce = 250.5f;

    [SerializeField]
    private float MoveForce = 4.5f;


    private Rigidbody2D Bod;

    private SpriteRenderer Rend;
    // [SerializeField]
    // private static float MaxMoveForce = 20.5f;
    void Start()
    {
        Bod = gameObject.GetComponent<Rigidbody2D>();
        Rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
        {
            // jump
            Bod.AddForce(new Vector2 (0f,JumpForce));
            //Debug.Log("Jump Plz");
        }

        if(keyboard != null && keyboard.dKey.isPressed)
        {
            Bod.AddForce(new Vector2(MoveForce, 0f));
            Rend.flipX = false;
        }

        if (keyboard != null && keyboard.aKey.isPressed)
        {
            Bod.AddForce(new Vector2((-MoveForce), 0f));
            Rend.flipX = true;
        }

        if (keyboard != null && keyboard.dKey.wasReleasedThisFrame)
        {
            Bod.AddForce(new Vector2((-MoveForce), 0f));
        }
        if (keyboard != null && keyboard.aKey.wasReleasedThisFrame)
        {
            Bod.AddForce(new Vector2((MoveForce), 0f));
        }


    }
}
