using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HUD_Control : MonoBehaviour
{

/*    private static HUD_Control _instance;
    public static HUD_Control Instance { get { return _instance; } }*/

    [SerializeField] private UIDocument UIDoc;

    [SerializeField] Sprite HeartImage;

    private VisualElement heartsContainer;
    private VisualElement root;



/*    private void Awake()
    {
        // check if it is the first time creating this singleton

        // we want to hold on to the game manager between levels while unity destroys everything else
        //when everything new is created we destroy the new one and retain this one
        if (_instance == null)
        {

            _instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //if an older game object exists destroy ourselves
            Destroy(this.gameObject);
        }
    }*/


    // Start is called before the first frame update
    void Start()
    {

        root = UIDoc.rootVisualElement;

        heartsContainer = root.Q<VisualElement>("Hearts-Container");

        UpdateHearts(heartsContainer,GameManager.Lives);

        GameManager.Instance.OnGamePaused.AddListener(Pause);
        GameManager.Instance.OnGameLose.AddListener(Pause);
        GameManager.Instance.OnGameResumed.AddListener(Play);
    }


    private void UpdateHearts(VisualElement HeartsContainer, int NumLives)
    {
        int numCurrent = HeartsContainer.childCount;



        if (NumLives < 0)
        {
            Debug.LogError("cannot have a negative amount of lives");
            GameManager.Lives = 0;
            return;
        }

        while (numCurrent > NumLives)
        {

            heartsContainer.RemoveAt(numCurrent - 1);
            numCurrent = HeartsContainer.childCount;

        }
        while (numCurrent < NumLives)
        {
            Image heart = new Image();
            heart.sprite = HeartImage;
            heart.style.paddingTop = 5;
            heart.style.paddingLeft = 0;
            heart.style.paddingRight = 0;
            heart.style.width = 64;
            heart.style.height = 64;
            heart.style.flexGrow = 0;
            heart.style.flexShrink = 0;
            //Debug.Log("go");
            heartsContainer.Add(heart);
            numCurrent = HeartsContainer.childCount;
        }

    }

    public void Pause()
    {

            /*SwitchActionMap(playerInput, "UI");*/
            // make the ui visible
            //root.style.visibility = Visibility.Visible;
            root.style.visibility = Visibility.Hidden;
            //paused = true;

    }

    public void Play()
    {

           /* SwitchActionMap(playerInput, "Player");*/
            root.style.visibility = Visibility.Visible;
            //paused = false;
    }

/*    private void SwitchActionMap(PlayerInput playerInput, string mapName)
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
    }*/

    // Update is called once per frame
    void Update()
    {
/*        if (!paused)
        {
            // now subtract how much time has elapsed
            timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);

            // update only about each second please        
            if (lastTimeLeft - timeLeft >= .9)
            {
                timeLabel.text = ((int)timeLeft).ToString();
                lastTimeLeft = timeLeft;
            }
        }*/
    }

    private void FixedUpdate()
    {
        UpdateHearts(heartsContainer, GameManager.Lives);
    }
}
