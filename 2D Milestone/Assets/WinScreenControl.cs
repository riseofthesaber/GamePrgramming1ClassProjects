using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WinScreenControl : MonoBehaviour
{
    [SerializeField] private UIDocument UIDoc;

    private VisualElement root;

    private Button menuButton;
    private Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        root = UIDoc.rootVisualElement;


        menuButton = root.Q<Button>("Menu");
        quitButton = root.Q<Button>("Quit");



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
