using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    [SerializeField] List<AchievementScriptableObject> achievements;

    public static AchievementHandler Instance = null;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void CheckForAchievement()
    {
        int currentScore = ScoreHandler.Instance.Score;
        int currentMaxPerfect = ScoreHandler.Instance.MaxPerfects;

        List<AchievementData> achievementDatas = new List<AchievementData>();
        bool changeInState = false;

        for (int i = 0; i < achievements.Count; i++)
        {
            if (!achievements[i].isUnlocked)
            {
                if (achievements[i].pointsToUnlock <= currentScore)
                {
                    if (achievements[i].perfectsToUnlock != 0 && achievements[i].perfectsToUnlock <= currentMaxPerfect)
                    {
                        achievements[i].isUnlocked = true;
                        Debug.Log("Unlocked "+achievements[i].name);
                        changeInState = true;
                    }
                    else if(achievements[i].perfectsToUnlock == 0)
                    {
                        achievements[i].isUnlocked = true;
                        Debug.Log("Unlocked " + achievements[i].name);
                        changeInState = true;
                    }
                }
            }
            AchievementData data = new AchievementData();
            data.achievementName = achievements[i].name;
            data.isEquipped = achievements[i].isEquipped;
            data.isUnlocked = achievements[i].isUnlocked;

            achievementDatas.Add(data);
        }

        Wrapper<AchievementData> wrapper = new Wrapper<AchievementData>();
        wrapper.items = achievementDatas.ToArray();

        string dataToSave = JsonUtility.ToJson(wrapper);
        Debug.Log(dataToSave);

        if(changeInState)
            SaveSystem.Save(dataToSave, ConfigData.achievementsFileName);
    }

    public Material GetEquippedMaterial()
    {
        foreach (AchievementScriptableObject achievement in achievements)
        {
            if (achievement.isEquipped)
            {
                return achievement.material;
            }                
        }
        return null;
    }
}
