using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] private UIDocument UIDoc;

    [SerializeField] HUDController hud;

    [SerializeField] PlayerInput playerInput;

    private VisualElement root;

    private Button resumeButton;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start hidden");
        root = UIDoc.rootVisualElement;
        root.style.visibility = Visibility.Hidden;

        resumeButton = root.Q<Button>("Resume-Button");

        resumeButton.clicked += buttonPlay;

    }

    public void buttonPlay()
    {
        Debug.Log("resume");
        SwitchActionMap(playerInput, "Player");
        root.style.visibility = Visibility.Hidden;
        hud.ButtonPlay();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchActionMap(playerInput, "UI");
            root.style.visibility = Visibility.Visible;
        }
    }

    public void Play(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchActionMap(playerInput, "Player");
            root.style.visibility = Visibility.Hidden;
        }
    }

    private void SwitchActionMap(PlayerInput playerInput, string mapName)
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(mapName);

    }

}
