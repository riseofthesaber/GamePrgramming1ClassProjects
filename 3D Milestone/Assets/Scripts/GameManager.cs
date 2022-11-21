using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private static int _lives = 3;
    public static int Lives { get { return _lives; } set { _lives = value; } }
    public enum GameState
    {
        Playing, Paused
    }
    public GameState CurrentGameState { get; private set; }

    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;
    public UnityEvent OnGameLose;

    private void Awake()
    {
        //check if we are the first one
        if (_instance == null){
            _instance = this;

            // were gonna stick around
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // we die and let the older one continue
            Destroy(this);
        }
    }

    public void ResumeGame()
    {
        CurrentGameState = GameState.Playing;
        Time.timeScale = 1f;
        OnGameResumed.Invoke();
    }
    public void PauseGame()
    {
        CurrentGameState = GameState.Paused;
        Time.timeScale = 0f;
        OnGamePaused.Invoke();  

    }

    public void TogglePause()
    {
        if(CurrentGameState == GameState.Paused)
        {
            ResumeGame();
        }else if (CurrentGameState == GameState.Playing)
        {
            PauseGame();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResumeGame();
    }

    public void LoseGame()
    {
        CurrentGameState = GameState.Paused;
        Time.timeScale = 0f;
        OnGameLose.Invoke();
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
