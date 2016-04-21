using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveData
{
    //The value in this class are public as it would make no sense to have them private as this class is only used as save class
    public ScoreBlock[] highScores;

    //Character/Ball Selected in shop Screen
    public int SelectedCharacter = 0;
    //Background/Stage Selected in shop Screen
    public int SelectedBackground = 0;
    public SaveData()
    {
        highScores = new ScoreBlock[] { new ScoreBlock(1000, "Kelling the mellon"), new ScoreBlock(750, "Skipper Pipper"), new ScoreBlock(500, "Mc ball"), new ScoreBlock(), new ScoreBlock(), new ScoreBlock(), new ScoreBlock(), new ScoreBlock(), new ScoreBlock(), new ScoreBlock() };
    }


    [System.Serializable]
    public struct ScoreBlock
    {
        public ScoreBlock(int score, string name)
        {
            this.score = score;
            this.name = name;
        }

        public int score;
        public string name;
    }
}


