using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BranchClass;
using StatsGridClass;
using TileClass;

namespace PlantClass
{
    public class PlantTree
    {
        public Branch Root;
        public StatsGrid Grid;
        public float Hits = 0;
        public int BranchCount = 1;

        public PlantTree(Vector2 start, float deg)
        {
            this.Root = new Branch(start, deg);
            this.Grid = new StatsGrid();
        }

        public PlantTree(Vector2 start, Vector2 end)
        {
            this.Root = new Branch(start, end);
            this.Grid = new StatsGrid();
        }

        public Branch GenBranch(List<Branch> tips)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Branch tip in tips)
            {
                Vector2 rounded = new Vector2(
                    Mathf.Round(tip.Start.x) + 10,
                    Mathf.Round(tip.Start.y) + 10
                );

                tiles.Add(this.Grid.Tiles[(int)rounded.x % 20, (int)rounded.y % 30]);
            }

            tiles.Sort(delegate (Tile t1, Tile t2)
            {
                return t1.Preferability.CompareTo(t2.Preferability);
            });

            Tile chosenTile;
            void chooseTile(int i)
            {
                if (Random.value < tiles[i].Preferability)
                {
                    chosenTile = tiles[i];
                }
                else
                {
                    i--;
                    if (i < 0)
                    {
                        chosenTile = tiles[0];
                    }
                    else
                    {
                        chooseTile(i);
                    }
                }
            }
            chooseTile(tiles.Count - 1);

            List<Branch> conflictingTips = new List<Branch>();
            foreach (Branch tip in tips)
            {
                Vector2 rounded = new Vector2(
                    Mathf.Round(tip.Start.x) + 10,
                    Mathf.Round(tip.Start.y) + 10
                );

                if (this.Grid.Tiles[(int)rounded.x % 20, (int)rounded.y % 30] == chosenTile)
                {
                    conflictingTips.Add(tip);
                }
            }
            Branch chosenTip = conflictingTips[Random.Range(0, conflictingTips.Count)];

            Branch continueBranch()
            {
                Branch newBranch = new Branch(chosenTip.End, chosenTile.Deg, chosenTip);
                chosenTip.Branches.Add(newBranch);
                tips.Add(newBranch);
                tips.Remove(chosenTip);

                return newBranch;
            }

            if (chosenTip.Parent != null)
            {
                if (Random.value < chosenTile.Branching && chosenTip.Parent.Branches.Count < 2)
                {
                    Vector2 rounded = new Vector2(
                        Mathf.Round(chosenTip.Parent.Start.x) + 10,
                        Mathf.Round(chosenTip.Parent.Start.y) + 10
                    );

                    float branchDeg = this.Grid.Tiles[(int)rounded.x % 20, (int)rounded.y % 30].BranchDeg;
                    Branch newBranch = new Branch(chosenTip.Parent.End, branchDeg, chosenTip.Parent);
                    chosenTip.Parent.Branches.Add(newBranch);
                    tips.Add(chosenTip.Parent.Branches[chosenTip.Parent.Branches.Count - 1]);

                    return newBranch;
                }
                else
                {
                    return continueBranch();
                }
            }
            else
            {
                return continueBranch();
            }
        }

        public PlantTree Dublicate()
        {
            PlantTree dublicate = new PlantTree(this.Root.Start, this.Root.End);

            dublicate.Root = this.Root;
            dublicate.Grid = this.Grid;
            dublicate.Hits = this.Hits;

            //dublicate.Grid.Randomize();

            return dublicate;
        }
    }
}