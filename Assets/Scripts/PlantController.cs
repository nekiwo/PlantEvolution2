using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantClass;
using BranchClass;

public class PlantController : MonoBehaviour
{
    public GameObject BranchTemplate;
    public GameObject Sun;

    public void RenderPlant(PlantTree plant)
    {
        RenderBranch(plant.Root);
    }

    private void RenderBranch(Branch main)
    {
        GameObject branchCopy = GameObject.Instantiate(BranchTemplate);
        branchCopy.transform.position = main.Start + (main.End - main.Start) / 2;
        branchCopy.transform.eulerAngles = new Vector3(0, 0,
            Mathf.Rad2Deg * -Mathf.Atan(
                (main.End.x - main.Start.x) /
                (main.End.y - main.Start.y)
            )
        );

        Debug.DrawLine(main.Start, main.End, Color.green, 100);

        main.Branches.ForEach(delegate (Branch branch)
        {
            RenderBranch(branch);
        });
    }
}
