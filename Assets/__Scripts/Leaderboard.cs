using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    public int maxEntries = 5;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    string GetKey(int rank)
    {
        string scene = SceneManager.GetActiveScene().name;
        return scene + "_score_" + rank;
    }

    public void SubmitScore(float timeUsed)
    {
        List<float> scores = GetScores();
        scores.Add(timeUsed);
        scores.Sort();
        if (scores.Count > maxEntries)
            scores.RemoveRange(maxEntries, scores.Count - maxEntries);
        for (int i = 0; i < scores.Count; i++)
            PlayerPrefs.SetFloat(GetKey(i), scores[i]);
        PlayerPrefs.Save();
    }

    public List<float> GetScores()
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < maxEntries; i++)
        {
            string key = GetKey(i);
            if (PlayerPrefs.HasKey(key))
                scores.Add(PlayerPrefs.GetFloat(key));
        }
        return scores;
    }

    public static List<float> LoadScoresForScene(string sceneName, int max = 5)
    {
        List<float> scores = new List<float>();
        for (int i = 0; i < max; i++)
        {
            string key = sceneName + "_score_" + i;
            if (PlayerPrefs.HasKey(key))
                scores.Add(PlayerPrefs.GetFloat(key));
        }
        return scores;
    }
}