using System;
using System.Collections;

[System.Serializable]
public class SaveData
{
    /// <summary>
    /// Value that contains all the highscore players submitted should have a size of 10
    /// </summary>
    public ScoreBlock[] highScores;
    
    /// <summary>
    /// Bools that says if a Charater on that index is unlocked yes or no
    /// </summary>
    public bool[] UnlockedCharacters = new bool[0];
    /// <summary>
    /// Bool that says if a Background on that index is unlocked yes or no
    /// </summary>
    public bool[] UnlockedBackgrounds = new bool[0];

    /// <summary>
    /// Current selected character
    /// </summary>
    public int SelectedCharacter = 0;

    /// <summary>
    /// The points the player can spent in the the store
    /// </summary>
    public int StorePoints = 0;

    /// <summary>
    /// That background that is currently selected
    /// </summary>
    public int SelectedBackground = 0;

    /// <summary>
    /// Used when creating a new blank savedata.
    /// </summary>
    public SaveData()
    {
        highScores = new ScoreBlock[] { new ScoreBlock(1000, "Kelling the mellon"), new ScoreBlock(750, "Skipper Pipper"), new ScoreBlock(500, "Mc ball"), new ScoreBlock(375,"Ewan Highwind"), new ScoreBlock(250, "Jan Cruck"), new ScoreBlock(125, "Putter Dutter"), new ScoreBlock(), new ScoreBlock(), new ScoreBlock(), new ScoreBlock() };
    }

    /// <summary>
    /// Struct that represents a single score in the highscore list
    /// </summary>
    [System.Serializable]
    public struct ScoreBlock
    {
        /// <summary>
        /// Constructor for Scoreblock
        /// </summary>
        /// <param name="score">Points scored that the player got durring there run</param>
        /// <param name="name">Name the player gave up when submitting there score to the highscore table</param>
        public ScoreBlock(int score, string name)
        {
            this.score = score;
            this.name = name;
        }

        public int score;
        public string name;
    }
}


