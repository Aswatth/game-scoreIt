using System.Collections.Generic;
using UnityEngine;

public class ConfigData
{
    public static readonly string achievementsFileName = "achievementData";
    public static readonly string bestScoreFileName = "bestScore";

    public static void SaveData(List<AchievementScriptableObject> achievements)
    {
        List<AchievementData> achievementDatas = new List<AchievementData>();

        for (int i = 0; i < achievements.Count; i++)
        {
            AchievementData data = new AchievementData();
            data.achievementName = achievements[i].name;
            data.isEquipped = achievements[i].isEquipped;
            data.isUnlocked = achievements[i].isUnlocked;

            achievementDatas.Add(data);
        }
        Wrapper<AchievementData> dataWrapper = new Wrapper<AchievementData>();
        dataWrapper.items = achievementDatas.ToArray();

        string dataToSave = JsonUtility.ToJson(dataWrapper);
        SaveSystem.Save(dataToSave, ConfigData.achievementsFileName);
    }

    public static List<AchievementScriptableObject> MapSavedData(List<AchievementData> savedData, List<AchievementScriptableObject>achievements)
    {
        for (int i = 0; i < savedData.Count; i++)
        {
            for (int j = 0; j < achievements.Count; j++)
            {
                if (achievements[j].name == savedData[i].achievementName)
                {
                    achievements[j].isEquipped = savedData[i].isEquipped;
                    achievements[j].isUnlocked = savedData[i].isUnlocked;

                    //if (achievements[j].isEquipped)
                    //{
                    //    Debug.Log(achievements[j].name);
                    //    //ballMeshRenderer.material = achievements[i].material;
                    //}
                    break;
                }
            }
        }
        return achievements;
    }
}