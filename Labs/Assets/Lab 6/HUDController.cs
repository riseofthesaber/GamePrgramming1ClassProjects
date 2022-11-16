using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{

    [SerializeField] private UIDocument UIDoc;
    [SerializeField] private float timeLeft;
    private float lastTimeLeft;
    [SerializeField] Sprite HeartImage;
    [SerializeField] private int NumLives = 3;
    [SerializeField] PlayerInput playerInput;
    // refrence to the hearts container to add a heart to
    private VisualElement heartsContainer;
    private VisualElement root;

    private Label timeLabel;

    private bool paused = false;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        root = UIDoc.rootVisualElement;

        heartsContainer = root.Q<VisualElement>("Lives");

        UpdateHearts(heartsContainer);



    }
    // life up nad life down exist in case anything else wants to add or remove a single life

    private void Awake()
    {
        // the Q function is for query, so we look in the tree for the 
        // element which is called "time-left", i.e., our time left label
        timeLabel = UIDoc.rootVisualElement.Q<Label>("time-left");

        // now set it to be the string representation of how much time is left
        timeLabel.text = ((int)timeLeft).ToString();

        // mark the last time we set this
        lastTimeLeft = timeLeft;
    }

    private void LifeUp()
    {
        NumLives++;
        UpdateHearts(heartsContainer);
    }
    private void LifeDown()
    {
        NumLives--;
        UpdateHearts(heartsContainer);
    }
    // these dont work for some reason, idk, i have tried everything i can tink of. i know it can read the keys beigh pressed because the other one works
    //i just dont know whare the disconeect i appening in unity with the input actions
    public void AddLife(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("go up");
            LifeUp();
        }
    }
    public void RemoveLife(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("go down");
            LifeDown();
        }
    }


    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchActionMap(playerInput, "UI");
            // make the ui visible
            //root.style.visibility = Visibility.Visible;
            root.style.visibility = Visibility.Hidden;
            paused = true;
        }
    }


    public void ButtonPlay()
    {
        SwitchActionMap(playerInput, "Player");
        root.style.visibility = Visibility.Visible;
        paused = false;
    }
    public void Play(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchActionMap(playerInput, "Player");
            root.style.visibility = Visibility.Visible;
            paused = false;
        }
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

    private void UpdateHearts(VisualElement HeartsContainer)
    {
        int numCurrent = HeartsContainer.childCount;



        if (NumLives < 0)
        {
            Debug.LogError("cannot have a negative amount of lives, setting lives to 0");
            NumLives = 0;
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

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            // now subtract how much time has elapsed
            timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);

            // update only about each second please        
            if (lastTimeLeft - timeLeft >= .9)
            {
                timeLabel.text = ((int)timeLeft).ToString();
                lastTimeLeft = timeLeft;
            }
        }
    }
}
