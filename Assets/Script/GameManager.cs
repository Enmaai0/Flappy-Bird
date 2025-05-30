using System.Collections;
using UnityEngine;

public enum GameState
{
    Ready,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("游戏状态")]
    public GameState currentState = GameState.Ready;

    [Header("引用")]
    public UIManager uiManager;
    public PlayerController player;
    public PipeManager pipeManager;
    public ScoreManager scoreManager;

    [Header("游戏结束设置")]
    public float gameOverDelay = 1f; // 游戏结束延迟时间
    private bool isGameOverProcessing = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // 设置目标帧率（安卓优化）
        Application.targetFrameRate = 60;
        // 防止屏幕休眠
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
        SetGameState(GameState.Ready);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentState)
            {
                case GameState.Ready:
                    StartGame();
                    break;
                case GameState.Playing:
                    player.Jump();
                    break;
                case GameState.GameOver:
                    if (!isGameOverProcessing)
                    {
                        RestartGame();
                    }
                    break;
            }
        }
    }

    private void SetGameState(GameState newState)
    {
        currentState = newState;
        uiManager.ChangeState(newState);
        switch (newState)
        {
            case GameState.Ready:
                player.SetReadyState();
                pipeManager.ClearPipes();
                break;
            case GameState.Playing:
                player.SetPlayingState();
                player.Jump();
                pipeManager.StartSpawn();
                break;
            case GameState.GameOver:
                player.SetGameOverState();
                pipeManager.StopSpawning();
                StartCoroutine(HandleGameOver());
                break;
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.Playing);
        scoreManager.ResetScore();
    }

    IEnumerator HandleGameOver()
    {
        isGameOverProcessing = true;
        yield return new WaitForSeconds(gameOverDelay);
        uiManager.ChangeState(GameState.GameOver);
        isGameOverProcessing = false;
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
        scoreManager.SaveBestScore();
    }

    public void RestartGame()
    {
        SetGameState(GameState.Ready);
    }

    public void AddScore()
    {
        scoreManager.AddScore();
    }
}
