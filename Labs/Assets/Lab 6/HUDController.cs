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
    public void AddLife(InputAction.CallbackContext context) {

        LifeUp();
    }
    public void RemoveLife(InputAction.CallbackContext context) {
        LifeDown();
    }

    private void UpdateHearts(VisualElement heartsContainer)
    {
        int numCurrent = heartsContainer.childCount;
        Image heart = new Image();
        heart.sprite = HeartImage;
        heart.style.paddingTop = 5;
        heart.style.paddingLeft = 0;
        heart.style.paddingRight = 0;
        heart.style.width = 64;
        heart.style.height = 64;
        heart.style.flexGrow = 0;
        heart.style.flexShrink = 0;

        while (numCurrent != NumLives)
        {
            if(NumLives<0)
            {
                Debug.LogError("cannot have a negative amount of lives, setting lives to 0");
                NumLives = 0;
                break;
            }
            if( NumLives < numCurrent)
            {
                heartsContainer.RemoveAt(numCurrent);
            }
            else
            {
                heartsContainer.Add(heart);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
