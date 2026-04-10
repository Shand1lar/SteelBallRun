using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Timer")]
    public float timeLimit = 90f;

    private float timeRemaining;
    private bool gameActive = false;
    private bool isPaused   = false;
    private PlayerBall playerBall;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = timeLimit;
        gameActive    = true;
        playerBall    = FindObjectOfType<PlayerBall>();
        UIManager.instance.ShowHUD();
    }

    void Update()
    {
        if (!gameActive) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else          PauseGame();
        }

        if (!isPaused)
        {
            timeRemaining -= Time.deltaTime;
            UIManager.instance.UpdateTimer(timeRemaining);

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                TriggerFailure();
            }
        }
    }

    public void PlayerReachedGoal()
    {
        if (!gameActive) return;
        gameActive = false;
        playerBall.inputEnabled = false;
        playerBall.FreezeVelocity();
        float timeUsed = timeLimit - timeRemaining;
        Leaderboard.instance.SubmitScore(timeUsed);
        UIManager.instance.ShowVictoryScreen(timeUsed);
    }

    void TriggerFailure()
    {
        gameActive = false;
        playerBall.inputEnabled = false;
        playerBall.FreezeVelocity();
        UIManager.instance.ShowFailureScreen();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        playerBall.inputEnabled = false;
        UIManager.instance.ShowPauseMenu();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        playerBall.inputEnabled = true;
        UIManager.instance.ShowHUD();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
        Time.timeScale = 1f;
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
        else
            SceneManager.LoadScene(0);
    }
}