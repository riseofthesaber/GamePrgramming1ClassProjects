using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseMenuControl : MonoBehaviour
{

    [SerializeField] private UIDocument UIDoc;

    [SerializeField] HUD_Control hud;

    [SerializeField] PlayerInput playerInput;

    private VisualElement root;

    private Button resumeButton;
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start hidden");
        root = UIDoc.rootVisualElement;
        root.style.visibility = Visibility.Hidden;

        resumeButton = root.Q<Button>("Resume");
        quitButton = root.Q<Button>("Quit");

        GameManager.Instance.OnGamePaused.AddListener(Pause);
        GameManager.Instance.OnGameResumed.AddListener(Play);

        resumeButton.clicked += buttonPlay;
        quitButton.clicked += buttonQuit;

    }

    private void OnDestroy()
    {
        if(resumeButton != null)
        {
            // need to unsubscribe or else errors
            resumeButton.clicked -= buttonPlay;
            resumeButton.clicked -= buttonQuit;
        }
    }


    public void buttonPlay()
    {
        /*        Debug.Log("resume");
                SwitchActionMap(playerInput, "Player");
                root.style.visibility = Visibility.Hidden;
                hud.Play();*/
        GameManager.Instance.ResumeGame();

    }

    public void buttonQuit()
    {
        SwitchActionMap(playerInput, "UI");
        GameManager.SwitchScene("Main Menu");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {

                SwitchActionMap(playerInput, "UI");
                root.style.visibility = Visibility.Visible;

    }

    public void Play()
    {

            //Debug.Log("resume");
            SwitchActionMap(playerInput, "Player");
            root.style.visibility = Visibility.Hidden;

        
    }

    private void SwitchActionMap(PlayerInput playerInput, string mapName)
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(mapName);

            switch (mapName)
            {
                case "UI":
                    UnityEngine.Cursor.visible = true;
                    UnityEngine.Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    UnityEngine.Cursor.visible = false;
                    UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                    break;
            }

        }
}
