using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using static GameManager;

public class LoseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UIDocument UIDoc;
    private VisualElement root;
    private Button PlayButton;


    void Start()
    {

        root = UIDoc.rootVisualElement;
        root.style.visibility = Visibility.Visible;
        PlayButton = root.Q<Button>("BackToMenu");
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        PlayButton.clicked += PlayButtonFunction;
    }

    private void PlayButtonFunction()
    {
        //throw new NotImplementedException();
        Debug.Log("Hey");
        GameManager.Lives = 3;
        GameManager.SwitchScene("MainMenu");
    }
    // Update is called once per frame

    private void OnDestroy()
    {
        PlayButton.clicked -= PlayButtonFunction;
    }

    void Update()
    {
        
    }
}
