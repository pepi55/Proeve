using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Events
{
    /// <summary>
    /// Called when the game requires a restart like when the player goes game over
    /// </summary>
    class IResetGameState : IEvent
    {
        /// <summary>
        /// Was the last score a highscore. 
        /// This value is used to let certain things happen when the player had a highscore
        /// </summary>
        public bool lastScoreWasHighScore = false;

        public IResetGameState (bool lastScoreWasHighScore)
        {
            this.lastScoreWasHighScore = lastScoreWasHighScore;
        }
    }
}
