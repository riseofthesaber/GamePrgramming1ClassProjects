using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [Header("Player Input")]
    [Tooltip("the movement input from our player")]
    [SerializeField] private Vector2 movementInput;

    [Tooltip("the movement input thats aligned with the camera direction")]
    [SerializeField] private Vector3 cameraAdjustedInputDirection;

    private PlayerInputActions playerInputActions;


    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraOrientation;


    [Header("Component/objct refrence")]
    [Tooltip("refrence to our movement component (drag here)")]
    [SerializeField] private Base_Movement characterMovement;

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

        characterMovement.Move(cameraAdjustedInputDirection);
    }
    private void JumpActionPreformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Hey");
        if (context.canceled) { characterMovement.JumpCanceled(); }
        else { characterMovement.Jump(); }
    }
    private void SubscribeInputActions()
    {
        playerInputActions.Player.Move.started += MoveActionPreformed;
        playerInputActions.Player.Move.performed += MoveActionPreformed;
        playerInputActions.Player.Move.canceled += MoveActionPreformed;

        playerInputActions.Player.Jump.started += JumpActionPreformed;
        playerInputActions.Player.Jump.canceled += JumpActionPreformed;
    }

    private void UnSubscribeInputActions()
    {
        playerInputActions.Player.Move.started -= MoveActionPreformed;
        playerInputActions.Player.Move.performed -= MoveActionPreformed;
        playerInputActions.Player.Move.canceled -= MoveActionPreformed;

        playerInputActions.Player.Jump.started -= JumpActionPreformed;
        playerInputActions.Player.Jump.canceled -= JumpActionPreformed;

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
        playerInputActions = new PlayerInputActions();
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
        
    }
}
