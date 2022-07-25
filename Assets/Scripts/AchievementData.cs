[System.Serializable]
public class AchievementData
{
    public string achievementName;
    public bool isEquipped;
    public bool isUnlocked;
}

public class BestScore
{
    public int bestScore;
}

[System.Serializable]
public class Wrapper<T>
{
    public T[] items;
}