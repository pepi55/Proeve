using UnityEngine;
using System.Collections;

namespace Events
{
    public class IBallMove : IEvent
    {
        public Vector2 direction;
        public Vector2 position;

        public IBallMove (Vector2 direction, Vector2 position)
        {
            this.direction = direction;
            this.position = position;
        }
    }
}
