using UnityEngine;

public static class GameProgressManager
{
    public static void CompleteLevel(int levelNumber)
    {
        PlayerPrefs.SetInt("Level" + levelNumber + "_Complete", 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelComplete(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "_Complete", 0) == 1;
    }
}
