using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // static is a value that exists in only the class
    // it only exists once so it can be accessed by name
    private static GameManager _instance;
    private static int _lives = 3;
    //
    public static GameManager Instance { get { return _instance; } }
    public static int Lives { get { return _lives; } set { _lives = value;   } }

    public enum GameState
    {
        Playing, Paused
    }

    public GameState CurrentGameState { get; private set; }

    // events for when the game is paused
    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;
    public UnityEvent OnGameLose;

    private void Awake()
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
    }

    // resumes gameplay
    public void ResumeGame()
    {
        // set current game state to playing
        CurrentGameState = GameState.Playing;

        Time.timeScale = 1.0f;

        // notify everything that is listening that it can resume playing
        OnGameResumed.Invoke();
    }
    public void PauseGame()
    {
        CurrentGameState = GameState.Paused;
        Time.timeScale = 0f;
        OnGamePaused.Invoke();
    }

    public void LoseGame()
    {
        CurrentGameState = GameState.Paused;
        Time.timeScale = 0f;
        OnGameLose.Invoke();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResumeGame();
    }

    public static void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);

    }

    void Start()
    {
        //gameManager.Instance
        // works just like Vector2.right 


    }

    // Update is called once per frame
    void Update()
    {

    }
}
