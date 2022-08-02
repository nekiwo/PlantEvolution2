using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BranchClass
{
    public class Branch
    {
        public Vector2 Start;
        public Vector2 End;
        public Branch Parent;
        public List<Branch> Branches;

        public Branch(Vector2 start, float deg, Branch parent = null)
        {
            this.Start = start;
            this.End = start + this.DegToVector(deg, 1);
            this.Parent = parent;
            this.Branches = new List<Branch>();
        }

        public Branch(Vector2 start, Vector2 end, Branch parent = null)
        {
            this.Start = start;
            this.End = end;
            this.Parent = parent;
            this.Branches = new List<Branch>();
        }

        public Vector2 DegToVector(float deg, float mag)
        {
            return new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad)) * mag;
        }
    }
}
