﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameLoop
{
    class MyGame
    {
        string CurrLine;
        List<Event> Events;
        bool IsRunning;
        List<Event> FiredEvents;
        bool userEntered;

        public MyGame()
        {

        }

        public void initialize()
        {
            Console.WriteLine("GameLoop Demo Initializing");
            Events = new List<Event>();
            FiredEvents = new List<Event>();
            userEntered = false;
            Console.Write("[cmd:] ");
        }

        public void run()
        {
            IsRunning = true;
            DateTime begin = DateTime.Now;
            while (IsRunning)
            {
                processInput();
                update(DateTime.Now-begin);
                render();
            }
        }

        public void update(TimeSpan elapsedTime)
        {

            List<Event> toRemove = new List<Event>();
            Events.ForEach(e =>
            {
                if (e.canFire(elapsedTime))
                {
                    FiredEvents.Add(e);
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
            if (FiredEvents.Count > 0)
            {
                FiredEvents.ForEach(e => {
                    Console.WriteLine("\n\tEvent " + e.Name + " (" + e.Times + " remaining)");
                });
                FiredEvents.Clear();
                Console.Write("[cmd:] " + CurrLine);
            }
            if (userEntered)
            {
                Console.Write("\n[cmd:] ");
                CurrLine = "";
                userEntered = false;
            }
        }

        public void processInput()
        {
            if (Console.KeyAvailable)
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
        }

        public void handleEnter()
        {
            if (CurrLine.Equals("quit"))
            {
                IsRunning = false;
            }
            else
            {
                Event e = Event.parseEvent(CurrLine);
                if (e != null)
                {
                    Events.Add(e);
                }
                userEntered = true;
            }
        }
    }
}
