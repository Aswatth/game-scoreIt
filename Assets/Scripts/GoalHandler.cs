using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Start timer after first goal
            if (!GameTimer.Instance.TimerStarted)
            {
                GameTimer.Instance.StartTimer();
            }
            //Reset timer for subsequent goals
            else
            {
                GameTimer.Instance.ResetTimer();
            }

            bool isPerfectShot = PerfectShotHandler.Instance.IsPerfectShot;
            int scoreToAdd = isPerfectShot?3:1;// 3 for perfect shot and 1 for any other shot

            //Celebrate for perfect shot along with additional score
            if (isPerfectShot)
            {
                PerfectShotHandler.Instance.Celebrate();
            }

            //Add score for successful scoring
            ScoreHandler.Instance.AddScore(scoreToAdd, isPerfectShot);

            PerfectShotHandler.Instance.ResetPerfectShot();

            //Check for achievement
            AchievementHandler.Instance.CheckForAchievement();

            //Spawn next goal post
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                RecklessGoalPostSpawner.Instance.SpawnGoalPost();
            }
            else if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                GoalPostSpawner.Instance.SpawnGoalPost();
            }
        }
    }
}
