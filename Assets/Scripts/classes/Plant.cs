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

        public PlantTree(Vector2 start, float deg)
        {
            this.Root = new Branch(start, deg);
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
             * - figure out tiles of all tips
             * - sort tiles by most preferability
             * - if random hits, choose tile
             * - else, the random for next most preferred tile (repeat until done)
             * - check if tile has multiple tips, then choose one tip at random
             * - if random hits branching, make new branch on previous branch (use stats of previous tile for deg)
             * - else, make new branch on top of current tip
             * - NOTE: update tips list and the plant object!!
             * 
             */

            remBranches--;
            if (remBranches > 0)
            {
                GenBranch(tips, remBranches);
            }
        }
    }
}