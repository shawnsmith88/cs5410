using System;
using System.Collections.Generic;

namespace Maze
{
    public class DogeSprite: GameObject
    {
        public List<BreadCrumb> BreadCrumbs;
        public bool ShowBreadCrumbs;
        public bool ShowBestPath;
        public List<Future> BestPath;
        public Future Hint;
        public bool ShowHint;

        public DogeSprite()
        {
            BreadCrumbs = new List<BreadCrumb>();
            BestPath = new List<Future>();
            ShowBreadCrumbs = false;
            ShowBestPath = false;
            ShowHint = false;
        }
    }
}