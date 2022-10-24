using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("the movement input from our player")]
    [SerializeField] private Vector2 movementInput;

    [Tooltip("the movement input thats aligned with the camera direction")]
    [SerializeField] private Vector3 cameraAdjustedInputDirection;

    private ControlInputs playerInputActions;


    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraOrientation;


    [Header("Component/objct refrence")]
    [Tooltip("refrence to our movement component (drag here)")]
    [SerializeField] private BaseMove characterMove;
    
    [Header("Character Animator")]
    [Tooltip("The animator used by this game object")]
    [SerializeField]
    private Animator animator;

    private void RotateCameraAndCharacter()
    {
        // subtract from transform.position  the camera transform. position x and z but set y to zero.
        // this takes the camera position's direction it is facing and makes the value have no rotation on th y axis
        Vector3 basicViewDir = transform.position - new Vector3(cameraTransform.position.x,
    transform.position.y, cameraTransform.position.z);

        // now set this object's forward direction as the direction the camera is facing so when the character moves forward
        // it moves toward where th camera is facing
        cameraOrientation.forward = basicViewDir.normalized;
        // now rotate the character towards Forward in Character move
        characterMove.RotateCharacter();
    }


    private void CalculateCameraRelativeInput()
    {

        cameraAdjustedInputDirection = cameraOrientation.forward * movementInput.y +
            cameraOrientation.right * movementInput.x; // if x is negative aka left, then camera orientation will become negative, aka left

        // possibly normalize if vector is too big
        if (cameraAdjustedInputDirection.sqrMagnitude > 1f)
        {
            cameraAdjustedInputDirection = cameraAdjustedInputDirection.normalized;
        }
    }
    private void MoveActionPreformed(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        // get teh relative input direction
        CalculateCameraRelativeInput();

        characterMove.Move(cameraAdjustedInputDirection);
    }

    private void DanceActionPreformed(InputAction.CallbackContext context)
    {
        characterMove.Dance();
    }

    private void JumpActionPreformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Hey");
        characterMove.Jump();
    }
    private void JumpActionCanceled(InputAction.CallbackContext context)
    {
        characterMove.JumpCanceled();
    }

    private void SubscribeInputActions()
    {
        playerInputActions.Player.Move.started += MoveActionPreformed;
        playerInputActions.Player.Move.performed += MoveActionPreformed;
        playerInputActions.Player.Move.canceled += MoveActionPreformed;

        playerInputActions.Player.Jump.started += JumpActionPreformed;
        playerInputActions.Player.Jump.canceled += JumpActionCanceled;
        playerInputActions.Player.Dance.started += DanceActionPreformed;
    }

    private void UnSubscribeInputActions()
    {
        playerInputActions.Player.Move.started -= MoveActionPreformed;
        playerInputActions.Player.Move.performed -= MoveActionPreformed;
        playerInputActions.Player.Move.canceled -= MoveActionPreformed;

        playerInputActions.Player.Jump.started -= JumpActionPreformed;
        playerInputActions.Player.Jump.canceled -= JumpActionCanceled;

    }

    private void SwitchActionMap(string mapName)
    {
        switch (mapName)
        {
            case "Player":
                playerInputActions.UI.Disable();
                playerInputActions.Player.Enable();
                break;
            case "UI":
                // fill this out yourself
                break;

        }

    }
    private void Awake()
    {
        playerInputActions = new ControlInputs();
        SubscribeInputActions();

        SwitchActionMap("Player");
    }

    private void OnDestroy()
    {
        UnSubscribeInputActions();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateCameraAndCharacter();

        CalculateCameraRelativeInput();
    }

    public void FixedUpdate()
    {
        characterMove.Move(cameraAdjustedInputDirection);
    }
}

