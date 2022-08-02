using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TileClass;

namespace StatsGridClass
{
    public class StatsGrid
    {
        public Tile[,] Tiles = new Tile[20, 30];

        public const int Deg = 0;
        public const int BranchDeg = 1;
        public const int Preferability = 2;
        public const int Branching = 3;

        public StatsGrid()
        {
            for (var i = 0; i < 600; i++)
            {
                this.Tiles[i / 30, i % 30] = new Tile();
            }
        }

        public void Randomize()
        {
            for (int i = 0; i < 600; i++)
            {
                this.Tiles[i / 30, i % 30].Randomize();
            }
        }

        public void Render(int type)
        {
            switch (type)
            {
                case Deg:
                    break;
                case BranchDeg:
                    break;
                case Preferability:
                    break;
                case Branching:
                    break;
            }
        }
    }
}
