using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Type", menuName = "Ball Type/New Ball Type")]
public class AchievementScriptableObject : ScriptableObject
{
    public Material material;
    public int pointsToUnlock;
    public int perfectsToUnlock;
    public bool isUnlocked;
    public bool isEquipped;
}
