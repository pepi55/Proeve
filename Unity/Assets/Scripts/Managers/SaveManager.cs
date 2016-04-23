using UnityEngine;
using System.Collections;

public static class SaveManager
{
    public static SaveData savaData { get { return _saveData; } }
    static SaveData _saveData;

    /// <summary>
    /// Loads up the savedata when ever this class is accesed for the first time
    /// </summary>
    static SaveManager()
    {
        _saveData = new SaveData();
        if(!Util.Serialization.Load("SavaData", Util.Serialization.fileTypes.text, ref _saveData))
        {
            _saveData = new SaveData();
            Save();
        }
    }

    /// <summary>
    /// Saves the save data to disk
    /// </summary>
    public static void Save()
    {
        if (_saveData != null)
        {
            Util.Serialization.Save("SavaData", Util.Serialization.fileTypes.text, _saveData);
        }
    }

    /// <summary>
    /// Adds new score to the core list
    /// </summary>
    /// <param name="Score">Score got</param>
    /// <param name="Name">Player name</param>
    public static void AddNewScore(int Score, string Name)
    {
        if (CheckNewScore(Score))
        {
            for (int i = 0; i < savaData.highScores.Length; i++)
            {
                if (Score > savaData.highScores[i].score)
                {
                    pushDown(i);
                    savaData.highScores[i].score = Score;
                    savaData.highScores[i].name = Name;
                    Save();
                    return;
                }
            }
            
        }      
    }

    /// <summary>
    /// Reorders list so the new score can be placed on the right position
    /// </summary>
    /// <param name="FromPosition">The location in the array where the new score will be placed</param>
    static void pushDown(int FromPosition)
    {
        for (int i = savaData.highScores.Length - 1; i > FromPosition; i--)
        {
            if (i - 1 >= 0)
            {
                savaData.highScores[i] = savaData.highScores[i - 1];
            }
        }
    }

    /// <summary>
    /// Checks if new value is higher than any contained in the high score list
    /// </summary>
    /// <param name="Score"></param>
    /// <returns>if score higher than any in the highscore list return true</returns>
    public static bool CheckNewScore(int Score)
    {
        for (int i = 0; i < savaData.highScores.Length; i++)
        {
            if (Score > savaData.highScores[i].score)
                return true;
        }
        return false;
    }
}
