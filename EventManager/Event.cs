using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager
{
    internal class Event
    {
        protected string description;
        protected DateTime startTime;
        protected DateTime endTime;

        public Event(string _description, DateTime _startTime, DateTime _endTime)
        {
            description = _description;
            startTime = _startTime;
            endTime = _endTime;
        }
        public string Description { get { return description; } set { description = value; } }
        public DateTime StartTime { get { return startTime; } set { startTime = value; } }
        public DateTime EndTime { get { return endTime; } set { endTime = value; } }

    }
}
