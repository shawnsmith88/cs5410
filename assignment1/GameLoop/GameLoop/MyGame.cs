using System;
using System.Collections.Generic;
using System.Text;

namespace GameLoop
{
    class MyGame
    {
        string CurrLine;
        List<Event> Events;
        bool IsRunning;
        Event FiredEvent;

        public MyGame()
        {

        }

        public void initialize()
        {
            Console.WriteLine("GameLoop Demo Initializing");
            Events = new List<Event>();
        }

        public void run()
        {
            Console.Write("[cmd:] ");
            IsRunning = true;
            DateTime begin = DateTime.Now;
            while (IsRunning)
            {
                update(DateTime.Now-begin);
            }
        }

        public void update(TimeSpan elapsedTime)
        {
            if (Console.KeyAvailable)
            {
                processInput();
            }

            bool fired = false;
            List<Event> toRemove = new List<Event>();
            Events.ForEach(e =>
            {
                if (e.canFire(elapsedTime))
                {
                    FiredEvent = e;
                    render();
                    if (e.Times <= 0)
                    {
                        toRemove.Add(e);
                    }
                }
            });

            toRemove.ForEach(e =>
            {
                Events.Remove(e);
            });
            toRemove.Clear();

            
        }

        public void render()
        {
            Console.WriteLine("\n\tEvent " + FiredEvent.Name + " (" + FiredEvent.Times + " remaining)");
            Console.Write("[cmd:] " + CurrLine);
        }

        public void processInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                handleEnter();
                return;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (CurrLine.Length > 0)
                {
                    CurrLine = CurrLine.Substring(0, CurrLine.Length - 1);
                    return;
                }
            }

            CurrLine += key.KeyChar;
        }

        public void handleEnter()
        {
            if (CurrLine.Equals("quit"))
            {
                IsRunning = false;
            }
            else
            {
                Events.Add(Event.parseEvent(CurrLine));
                Console.Write("\n[cmd:] ");
                CurrLine = "";
            }
        }
    }
}
