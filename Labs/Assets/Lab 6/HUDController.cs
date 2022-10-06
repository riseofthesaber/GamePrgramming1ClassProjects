using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{

    [SerializeField] private UIDocument UIDoc;
    [SerializeField] private float LevelTime;
    [SerializeField] Sprite HeartImage;
    [SerializeField] private int NumLives=3;
    // refrence to the hearts container to add a heart to
    private VisualElement heartsContainer;

    private Label timeLabel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        VisualElement root = UIDoc.rootVisualElement;

        heartsContainer = root.Q<VisualElement>("Lives");

        UpdateHearts(heartsContainer);
    }
    // life up nad life down exist in case anything else wants to add or remove a single life
    private void LifeUp() {
        NumLives++;
        UpdateHearts(heartsContainer);
    }
    private void LifeDown() { 
        NumLives--;
        UpdateHearts(heartsContainer);
    }
    // these dont work for some reason, idk, i have tried everything i can tink of. i know it can read the keys beigh pressed because the other one works
    //i just dont know whare the disconeect i appening in unity with the input actions
    public void AddLife(InputAction.CallbackContext context) {
        LifeUp();
        if (context.performed)
        {
            Debug.Log("go up");
        }
    }
    public void RemoveLife(InputAction.CallbackContext context) {
        LifeDown();
        if (context.performed)
        {
            Debug.Log("go down");
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

                    heartsContainer.RemoveAt(numCurrent-1);
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
        Keyboard kb = Keyboard.current;

        if (kb.digit1Key.wasPressedThisFrame)
        {
            LifeUp();
            //Debug.Log("heh");
        }
        if (kb.digit2Key.wasPressedThisFrame)
        {
            LifeDown();
            //Debug.Log("kek");
        }
    }
}
