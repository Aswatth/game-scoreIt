using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AchievementNavigation : MonoBehaviour
{
    [SerializeField] List<AchievementScriptableObject> achievements;

    [SerializeField] MeshRenderer ballMeshRenderer;

    [SerializeField] GameObject equipButton;
    [SerializeField] GameObject equippedIcon;
    [SerializeField] TMP_Text unlockInfo;

    Material currentMaterial;
    int equippedIndex;
    int currentIndex;

    private void Start()
    {
        Initialize();        
    }

    public void Initialize()
    {
        string loadedData = SaveSystem.Load(ConfigData.achievementsFileName);
        Wrapper<AchievementData> wrapper = JsonUtility.FromJson<Wrapper<AchievementData>>(loadedData);

        if (wrapper == null)
        {
            ConfigData.SaveData(achievements);

            InitialSetUp();
        }
        else
        {
            List<AchievementData> savedData = new List<AchievementData>(wrapper.items);

            achievements = ConfigData.MapSavedData(savedData,achievements);

            InitialSetUp();
        }
    }

    private void InitialSetUp()
    {
        foreach (AchievementScriptableObject item in achievements)
        {
            if (item.isEquipped)
            {
                currentMaterial = item.material;
                ballMeshRenderer.material = currentMaterial;
                currentIndex = achievements.IndexOf(item);
                equippedIndex = currentIndex;

                UpdateUI(item);

                break;
            }
        }
    }

    public void GoNext()
    {
        currentIndex = (currentIndex + 1) % achievements.Count;

        AchievementScriptableObject obj = achievements[currentIndex];

        currentMaterial = obj.material;
        ballMeshRenderer.material = currentMaterial;

        UpdateUI(obj);
    }

    public void GoPrevious()
    {
        currentIndex = (currentIndex - 1 < 0? achievements.Count - 1: currentIndex - 1) % achievements.Count;

        AchievementScriptableObject obj = achievements[currentIndex];

        currentMaterial = obj.material;
        ballMeshRenderer.material = currentMaterial;

        UpdateUI(obj);
    }

    public void Equip()
    {
        achievements[currentIndex].isEquipped = true;
        achievements[equippedIndex].isEquipped = false;

        equippedIndex = currentIndex;

        unlockInfo.enabled = false;
        equipButton.SetActive(false);
        equippedIcon.SetActive(true);

        ConfigData.SaveData(achievements);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateUI(AchievementScriptableObject obj)
    {
        if(obj.isEquipped && obj.isUnlocked)
        {
            unlockInfo.enabled = false;
            equipButton.SetActive(false);
            equippedIcon.SetActive(true);
        }
        if (!obj.isUnlocked)
        {
            unlockInfo.enabled = true;
            equipButton.SetActive(false);
            equippedIcon.SetActive(false);

            string info = "";

            if(obj.pointsToUnlock != 0)
                info += "Get " + obj.pointsToUnlock + " points";

            if (obj.perfectsToUnlock != 0)
            {
                if(obj.pointsToUnlock != 0)
                    info += "\nAnd " + obj.perfectsToUnlock + " perfects to unlock";
                else
                {
                    info += "Get " + obj.perfectsToUnlock + " perfects to unlock";
                }
            }
            else
            {
                info += " to unlock";
            }

            unlockInfo.text = info;
        }
        else if(!obj.isEquipped)
        {
            unlockInfo.enabled = false;
            equipButton.SetActive(true);
            equippedIcon.SetActive(false);
        }
    }
}
