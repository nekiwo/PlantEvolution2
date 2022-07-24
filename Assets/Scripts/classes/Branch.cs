using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BranchClass
{
    public class Branch
    {
        public Vector2 start;
        public Vector2 end;
        public List<Branch> branches;

        public Branch(Vector2 start, float deg)
        {
            this.start = start;
            this.end = start + this.DegToVector(deg, 1);
            this.branches = new List<Branch>();
        }

        public Vector2 DegToVector(float deg, float mag)
        {
            return new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad)) * mag;
        }
    }
}
