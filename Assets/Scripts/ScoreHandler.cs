using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] int perfectMultiplier;

    int maxScore = 0;
    int maxPerfects = 0;

    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_Text perfectUI;

    public static ScoreHandler Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        string savedData = SaveSystem.Load(ConfigData.bestScoreFileName);


        if(savedData.Length != 0)
        {
            BestScore bestScore = JsonUtility.FromJson<BestScore>(savedData);

            maxScore = bestScore.bestScore;
        }
        else
        {
            maxScore = 0;
        }
        maxPerfects = 0;
    }

    public void AddScore(int scoreToAdd, bool isPerfectShot)
    {
        score += scoreToAdd;
        scoreUI.text = score.ToString();

        if(score > maxScore)
        {
            maxScore = score;
            BestScore bestScore = new BestScore();
            bestScore.bestScore = maxScore;
            SaveSystem.Save(JsonUtility.ToJson(bestScore), ConfigData.bestScoreFileName);
        }

        if(isPerfectShot)
        {
            ++perfectMultiplier;
            perfectUI.enabled = true;
            perfectUI.text = "Perfect x" + perfectMultiplier;
            if (perfectMultiplier > maxPerfects)
            {
                maxPerfects = perfectMultiplier;
            }
        }
        else
        {
            perfectMultiplier = 0;
            perfectUI.enabled = false;
        }
        
    }
    public int Score
    {
        get 
        {
            return score;
        }
    }

    public int MaxScore
    {
        get
        {
            return maxScore;
        }
    }

    public int MaxPerfects
    {
        get
        {
            return maxPerfects;
        }
    }
}
