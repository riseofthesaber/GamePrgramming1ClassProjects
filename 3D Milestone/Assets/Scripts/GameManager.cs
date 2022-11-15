using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static GameManager Instance { get { return _instance; } }

    public enum GameState
    {
        Playing, Paused
    }
    public GameState CurrentGameState { get; private set; }

    public UnityEvent OnGamePaused;
    public UnityEvent OnGameResumed;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
