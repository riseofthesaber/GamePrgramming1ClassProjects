using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DefeatControl : MonoBehaviour
{
    [SerializeField] private UIDocument UIDoc;

    private VisualElement root;

    private Button menuButton;
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        root = UIDoc.rootVisualElement;
        root.style.visibility = Visibility.Hidden;

        menuButton = root.Q<Button>("Main");
        quitButton = root.Q<Button>("Quit");

        GameManager.Instance.OnGameLose.AddListener(died);

        menuButton.clicked += buttonPlay;
        quitButton.clicked += buttonQuit;
    }

    public void buttonPlay()
    {
        //Debug.Log("Start button pressed!");
        GameManager.SwitchScene("Main Menu");

    }

    public void buttonQuit()
    {
        Application.Quit();

    }

    public void died()
    {
        root.style.visibility = Visibility.Visible;
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    private void OnDestroy()
    {
        menuButton.clicked -= buttonPlay;
        quitButton.clicked -= buttonQuit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
