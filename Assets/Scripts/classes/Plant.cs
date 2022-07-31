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

        public PlantTree(Vector2 start, float deg)
        {
            this.Root = new Branch(start, deg);
            this.Grid = new StatsGrid();
        }

        public void GenPlant()
        {
            List<Branch> branchTips = new List<Branch>();
            branchTips.Add(this.Root);
            GenBranch(branchTips, Main.StartBranchCount);
        }

        public void GenBranch(List<Branch> tips, int remBranches)
        {
            /* TODO:
             * 
             * * figure out tiles of all tips
             * * sort tiles by most preferability
             * 
             * * if random hits, choose tile
             * * else, the random for next most preferred tile (repeat until done)
             * 
             * * check if tile has multiple tips, then choose one tip at random
             * 
             * - if random hits branching, make new branch on previous branch (use stats of previous tile for deg)
             * - else, make new branch on top of current tip
             * 
             * - NOTE: update tips list and the plant object!!
             * 
             */

            List<Tile> tiles = new List<Tile>();
            foreach (Branch tip in tips)
            {
                Vector2 rounded = new Vector2(
                    Mathf.Round(tip.Start.x) + 10,
                    Mathf.Round(tip.Start.y) + 10
                );

                tiles.Add(this.Grid.Tiles[(int)rounded.x, (int)rounded.y]);
            }

            tiles.Sort(delegate (Tile t1, Tile t2)
            {
                return t1.Preferability.CompareTo(t2.Preferability);
            });
            Debug.Log(tiles);

            Tile chosenTile;
            void chooseTile(int i)
            {
                if (Random.value < tiles[i].Preferability)
                {
                    chosenTile = tiles[i];
                } else
                {
                    i--;
                    if (i < 0)
                    {
                        chosenTile = tiles[0];
                    } else
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

                if (this.Grid.Tiles[(int)rounded.x, (int)rounded.y] == chosenTile)
                {
                    conflictingTips.Add(tip);
                }
            }
            Branch chosenTip = conflictingTips[Random.Range(0, conflictingTips.Count)];
            tips.Remove(chosenTip);

            if (chosenTip.Parent != null)
            {
                if (Random.value > chosenTile.Branching)
                {
                    chosenTip.Branches.Add(new Branch(chosenTip.End, chosenTile.Deg, chosenTip));
                    tips.Add(chosenTip.Branches[0]);
                } else
                {

                }
            } else
            {
                chosenTip.Branches.Add(new Branch(chosenTip.End, chosenTile.Deg, chosenTip));
                tips.Add(chosenTip.Branches[0]);
            }

            remBranches--;
            if (remBranches > 0)
            {
                GenBranch(tips, remBranches);
            }
        }
    }
}