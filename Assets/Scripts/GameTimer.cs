using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float gameTime;
    float currentTime;

    [SerializeField] Color defaultColor;
    [SerializeField] Color cautionColor;

    [SerializeField] Image timerUI;
    [SerializeField] Animator timerAnimation;

    private bool timerStarted = false;
    public static GameTimer Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if(timerStarted)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;

                float fillAmount = currentTime / gameTime;
                timerUI.fillAmount = fillAmount;

                if(fillAmount < 0.5f)
                {
                    timerAnimation.enabled = true;
                    timerUI.color = cautionColor;
                }
                else
                {
                    timerAnimation.enabled = false;
                }
            }
            else
            {
                Debug.Log("Time up");
                GameOverHandler.Instance.EndGame();
            }
        }
    }

    public bool TimerStarted
    {
        get
        {
            return timerStarted;
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
        currentTime = gameTime;
        //StartCoroutine(Timer());
    }

    public void ResetTimer()
    {
        currentTime = gameTime;
        timerUI.color = defaultColor;

        if (ScoreHandler.Instance.Score > 25)
        {
            if (gameTime > 7)
                gameTime -= 2;
        }
        if (ScoreHandler.Instance.Score > 20)
        {
            if (gameTime > 8)
                --gameTime;
        }
        if (ScoreHandler.Instance.Score > 10)
        {
            if(gameTime > 9)
                --gameTime;
        }
    }
}
