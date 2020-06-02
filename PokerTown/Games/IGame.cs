﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PokerTown.Games
{
    public interface IGame
    {
        public string Name { get; }
        public string ShortDescription { get; }
        public string LongDescription { get; }

        public void Execute();
    }
}
