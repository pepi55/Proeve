using UnityEngine;
using System.Collections;

namespace Events
{
    /// <summary>
    /// Called when the ball moves
    /// </summary>
    public class IBallMove : IEvent
    {
        /// <summary>
        /// Current direction the ball moves in
        /// Values are between -1 and 1
        /// </summary>
        public Vector2 direction = Vector2.zero;
        /// <summary>
        /// The position of the ball at the moment of clicking
        /// </summary>
        public Vector2 position = Vector2.zero;


        public IBallMove (Vector2 direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
        }
    }
}
