using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantClass;
using BranchClass;

public class PlantController : MonoBehaviour
{
    public GameObject BranchTemplate;
    public GameObject sun;

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
