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
    }
}
