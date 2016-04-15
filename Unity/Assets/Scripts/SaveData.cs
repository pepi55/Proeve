using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveData
{
    public ScoreBlock[] highScores;

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


