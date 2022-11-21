using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    [SerializeField] private UIDocument UIDoc;

    [SerializeField] Sprite HeartImage;

    private VisualElement heartsContainer;
    private VisualElement root;


    void Start()
    {

        root = UIDoc.rootVisualElement;

        heartsContainer = root.Q<VisualElement>("Hearts-Container");

        UpdateHearts(heartsContainer, GameManager.Lives);

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

    private void FixedUpdate()
    {
        UpdateHearts(heartsContainer, GameManager.Lives);
    }
}

