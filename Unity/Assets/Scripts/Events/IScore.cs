using UnityEngine;
using System.Collections;
namespace Events
{
    /// <summary>
    /// Called when the player hits the target
    /// </summary>
    public class IScore : IEvent
    {
        public int bounces = 0;
        public Vector2 lastDir = Vector2.zero;

        public IScore(int bounces, Vector2 lastDir)
        {
            this.bounces = bounces;
            this.lastDir = lastDir;
        }
    }
}