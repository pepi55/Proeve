using UnityEngine;
using System.Collections;

public static class SaveManager
{
    public static SaveData savaData { get { return _saveData; } }
    static SaveData _saveData;

    static SaveManager()
    {
        _saveData = new SaveData();
        if(!Util.Serialization.Load("SavaData", Util.Serialization.fileTypes.text, ref _saveData))
        {
            _saveData = new SaveData();
            Save();
        }
    }

    static void Save()
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
                if (Score < savaData.highScores[i].score)
                {
                    savaData.highScores[i].score = Score;
                    savaData.highScores[i].name = Name;
                    return;
                }
            }
            Save();
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
            if (Score < savaData.highScores[i].score)
                return true;
        }
        return false;
    }
}
