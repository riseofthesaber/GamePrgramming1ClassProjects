using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{

    [SerializeField] private UIDocument UIDoc;
    // Start is called before the first frame update
    private VisualElement root;
    private Button resumeButton;
    private Button quitButton;

    void Start()
    {

        root = UIDoc.rootVisualElement;
        root.style.visibility = Visibility.Hidden;
        resumeButton = root.Q<Button>("Resume");
        quitButton = root.Q<Button>("Quit");

        GameManager.Instance.OnGamePaused.AddListener(Pause);
        GameManager.Instance.OnGameLose.AddListener(Pause);
        GameManager.Instance.OnGameResumed.AddListener(Play);

        resumeButton.clicked += resumeButtonFunction;
        quitButton.clicked += quitButtonFunction;
    }

    private void quitButtonFunction()
    {
        //throw new NotImplementedException();
        Debug.Log("Hey");
        GameManager.SwitchScene("MainMenu");
    }

    private void resumeButtonFunction()
    {
        //throw new NotImplementedException();
        //SwitchActionMap("Player");
        GameManager.Instance.ResumeGame();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Pause()
    {

        /*SwitchActionMap(playerInput, "UI");*/
        // make the ui visible
        //root.style.visibility = Visibility.Visible;
        root.style.visibility = Visibility.Visible;
        //paused = true;

    }

    public void Play()
    {

        /* SwitchActionMap(playerInput, "Player");*/
        root.style.visibility = Visibility.Hidden;
        //paused = false;
    }
}
