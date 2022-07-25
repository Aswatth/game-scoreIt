using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] List<AchievementScriptableObject> achievements;
    [SerializeField] MeshRenderer ballMeshRenderer;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        string loadedData = SaveSystem.Load(ConfigData.achievementsFileName);
        Wrapper<AchievementData> wrapper = JsonUtility.FromJson<Wrapper<AchievementData>>(loadedData);
        
        //File does not exist or does not contain any data
        if (wrapper == null)
        {
            ConfigData.SaveData(achievements);
        }
        else
        {
            List<AchievementData> savedData = new List<AchievementData>(wrapper.items);

            achievements = ConfigData.MapSavedData(savedData, achievements);

            foreach (AchievementScriptableObject achievement in achievements)
            {
                if (achievement.isEquipped)
                {
                    ballMeshRenderer.material = achievement.material;
                    break;
                }
            }
        }
        
    }

    public void Achievements()
    {
        SceneManager.LoadScene(1);
    }

    public void DefaultGame()
    {
        SceneManager.LoadScene(2);
    }

    public void RecklessGame()
    {
        SceneManager.LoadScene(3);
    }
}
