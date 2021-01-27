using System;
using System.Collections.Generic;
using System.Text;

namespace GameLoop
{
    class Event
    {
        public string Name { get; }
        long Interval { get; }
        int Times { get; }
        public Event(string name, string interval, string times)
        {
            Name = name;
            Interval = long.Parse(interval);
            Times = int.Parse(times);
        }

        public bool canFire(TimeSpan elapsedTime)
        {
            return false;
        }

        public static Event parseEvent(string eventString)
        {
            string[] strs = eventString.Split(' ');
            return new Event(strs[2], strs[3], strs[4]);
        }
      
    }
}
