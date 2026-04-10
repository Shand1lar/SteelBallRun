using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Panels")]
    public GameObject hudPanel;
    public GameObject pausePanel;
    public GameObject victoryPanel;
    public GameObject failurePanel;

    [Header("HUD")]
    public Text timerText;

    [Header("Victory Screen")]
    public Text victoryTimeText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void HideAllPanels()
    {
        hudPanel.SetActive(false);
        pausePanel.SetActive(false);
        victoryPanel.SetActive(false);
        failurePanel.SetActive(false);
    }

    public void ShowHUD()
    {
        HideAllPanels();
        hudPanel.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        HideAllPanels();
        pausePanel.SetActive(true);
    }

    public void ShowVictoryScreen(float timeUsed)
    {
        HideAllPanels();
        victoryPanel.SetActive(true);
        victoryTimeText.text = "Your Time: " + timeUsed.ToString("F2") + "s";
    }

    public void ShowFailureScreen()
    {
        HideAllPanels();
        failurePanel.SetActive(true);
    }

    public void UpdateTimer(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.color = timeRemaining <= 10f ? Color.red : Color.white;
    }
}