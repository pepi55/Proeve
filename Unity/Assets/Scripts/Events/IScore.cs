using UnityEngine;
using System.Collections;
namespace Events
{
    /// <summary>
    /// Called when the player hits the target
    /// </summary>
    public class IScore : IEvent
    {
        /// <summary>
        /// Number of times the player bouched against the wall
        /// bonus points are given when you hit the walls when you score
        /// </summary>
        public int bounces = 0;
        /// <summary>
        /// The direction the player last clicked.
        /// this is so the player can get a bonus if they did a dunk
        /// </summary>
        public Vector2 lastDir = Vector2.zero;

        public IScore(int bounces, Vector2 lastDir)
        {
            this.bounces = bounces;
            this.lastDir = lastDir;
        }
    }
}