using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public GameObject BranchTemplate;
    public GameObject sun;

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

    public class PlantTree
    {
        public Branch root;

        public PlantTree(Vector2 start, float deg)
        {
            this.root = new Branch(start, deg);
        }
    }

    public void RenderPlant(PlantTree plant)
    {
        RenderBranch(plant.root);
    }

    private void RenderBranch(Branch main)
    {
        GameObject branchCopy = GameObject.Instantiate(BranchTemplate);
        branchCopy.transform.position = main.start + (main.end - main.start) / 2;
        branchCopy.transform.eulerAngles = new Vector3(0, 0,
            Mathf.Rad2Deg * -Mathf.Atan(
                (main.end.x - main.start.x) /
                (main.end.y - main.start.y)
            )
        );

        Debug.DrawLine(main.start, main.end, Color.green, 100);

        main.branches.ForEach(delegate (Branch branch)
        {
            RenderBranch(branch);
        });
    }
}
