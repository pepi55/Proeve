using UnityEngine;
using System.Collections;
namespace Events
{
    public class IPause : IEvent
    {
        public bool State;

        public IPause(bool State)
        {
            this.State = State;
        }
    }
}