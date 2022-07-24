using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BranchClass;

namespace PlantClass
{
    public class PlantTree
    {
        public Branch root;

        public PlantTree(Vector2 start, float deg)
        {
            this.root = new Branch(start, deg);
        }

        public void GenPlant()
        {
            List<Branch> branchTips = new List<Branch>();
            branchTips.Add(this.root);
            GenBranch(branchTips, Main.StartBranchCount);
        }

        public void GenBranch(List<Branch> tips, int remBranches)
        {
            // TODO: figure out the tiles of tips
            // TODO: compare tile stats of tip tiles
            // TODO: continue branch or add depending on stats

            remBranches--;
            if (remBranches > 0)
            {
                GenBranch(tips, remBranches);
            }
        }
    }
}