using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TileClass
{
    public class Tile
    {
        public float Deg;
        public float BranchDeg;
        public float Preferability;
        public float Branching;

        public Tile()
        {
            this.Deg = Random.Range(0f, 360f);
            this.BranchDeg = Random.Range(0f, 360f);
            this.Preferability = Random.value;
            this.Branching = Random.value;
        }

        public void Randomize()
        {
            this.Deg = (this.Deg + Random.Range(0, 360f * Main.MutationValue) - 180) % 360;
            this.BranchDeg = (this.BranchDeg + Random.Range(0, 360f * Main.MutationValue) - 180) % 360;
            this.Preferability = (this.Preferability + Random.value * Main.MutationValue - 0.5f) % 1;
            this.Branching = (this.Preferability + Random.value * Main.MutationValue - 0.5f) % 1;
        }
    }
}
