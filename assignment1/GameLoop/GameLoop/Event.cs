using System;
using System.Collections.Generic;
using System.Text;

namespace GameLoop
{
    class Event
    {
        public string Name { get; }
        public double Interval { get; }
        double lastIntervalTaken;
        public int Times { get; set; }
        public Event(string name, string interval, string times)
        {
            lastIntervalTaken = -1;
            Name = name;
            Interval = double.Parse(interval);
            Times = int.Parse(times);
        }

        public bool canFire(TimeSpan elapsedTime)
        {
            if (lastIntervalTaken == -1)
            {
                lastIntervalTaken = elapsedTime.TotalMilliseconds;
            }

            if (elapsedTime.TotalMilliseconds > lastIntervalTaken + Interval)
            {
                lastIntervalTaken = elapsedTime.TotalMilliseconds;
                Times--;
                return true;
            }

            return false;
        }

        public static Event parseEvent(string eventString)
        {
            string[] strs = eventString.Split(' ');
            return new Event(strs[2], strs[3], strs[4]);
        }
      
    }
}
