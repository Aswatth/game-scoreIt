using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject gameOverUI;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bestscoreText;
    [SerializeField] TMP_Text perfectsText;

    public static GameOverHandler Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void EndGame()
    {
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);

        DragAndShoot.Instance.enabled = false;
        if (SceneManager.GetActiveScene().buildIndex == 2)
            GoalPostSpawner.Instance.enabled = false;
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            RecklessGoalPostSpawner.Instance.enabled = false;

        GameTimer.Instance.enabled = false;

        scoreText.text = "Score: "+ScoreHandler.Instance.Score.ToString();
        bestscoreText.text = "Best: " + ScoreHandler.Instance.MaxScore.ToString();
        perfectsText.text = "Perfects: " + ScoreHandler.Instance.MaxPerfects.ToString();
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(2);
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            SceneManager.LoadScene(3);
    }
}
