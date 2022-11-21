using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private UIDocument UIDoc;
    private VisualElement root;
    private VisualElement cred;

    private Button startButton;
    private Button quitButton;
    private Button creditsButton;

    // Start is called before the first frame update
    void Start()
    {
        root = UIDoc.rootVisualElement;
        cred = root.Q<VisualElement>("CreditsBox");
        startButton = root.Q<Button>("Start");
        quitButton = root.Q<Button>("Quit");
        creditsButton = root.Q<Button>("Credits");

        cred.visible=false;

        startButton.clicked += buttonPlay;
        quitButton.clicked += buttonQuit;
        creditsButton.clicked += buttonCred;
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    public void buttonPlay()
    {
        //Debug.Log("Start button pressed!");
        GameManager.Instance.ResumeGame();
        GameManager.Lives = 3;
        GameManager.SwitchScene("Starting Scene");

    }

    public void buttonQuit()
    {
        Application.Quit();

    }


    public void buttonCred()
    {
        if (cred.visible == true)
        {
            cred.visible = false;
        }
        else
        {
            cred.visible = true;
        }

    }

    private void OnDestroy()
    {
        startButton.clicked -= buttonPlay;
        quitButton.clicked -= buttonQuit;
        creditsButton.clicked -= buttonCred;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
