using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject levelSelectPanel;
    public GameObject leaderboardPanel;

    [Header("Leaderboard UI")]
    public TextMeshProUGUI leaderboardText;


    public void ShowLevelSelect()
    {
        mainPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void ShowLeaderboard()
    {
        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        PopulateLeaderboard();
    }

    public void ShowMainMenu()
    {
        levelSelectPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel_B_03()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void PopulateLeaderboard()
    {
        List<float> scores = Leaderboard.LoadScoresForScene("Level_01");
        if (scores.Count == 0)
        {
            leaderboardText.text = "No times recorded yet!";
            return;
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("--- Level 1 ---");
        for (int i = 0; i < scores.Count; i++)
            sb.AppendLine((i + 1) + ".   " + scores[i].ToString("F2") + "s");
        leaderboardText.text = sb.ToString();
    }
}