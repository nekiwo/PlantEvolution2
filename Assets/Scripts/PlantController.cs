using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantClass;
using BranchClass;

public class PlantController : MonoBehaviour
{
    public GameObject BranchTemplate;
    public GameObject Sun;

    public void RenderPlant(Branch startingBranch, bool continueBranch)
    {
        renderBranch(startingBranch, continueBranch);
    }

    private void renderBranch(Branch main, bool continueBranch)
    {
        GameObject branchCopy = GameObject.Instantiate(BranchTemplate);
        branchCopy.transform.position = main.Start + (main.End - main.Start) / 2;
        branchCopy.transform.eulerAngles = new Vector3(0, 0,
            Mathf.Rad2Deg * -Mathf.Atan(
                (main.End.x - main.Start.x) /
                (main.End.y - main.Start.y)
            )
        );

        //Debug.DrawLine(main.Start, main.End, Color.green, Time.deltaTime);

        if (continueBranch)
        {
            main.Branches.ForEach(delegate (Branch branch)
            {
                renderBranch(branch, continueBranch);
            });
        }
    }

    public void RemovePlant()
    {
        foreach (GameObject branch in GameObject.FindGameObjectsWithTag("plant"))
        {
            if (branch != BranchTemplate)
            {
                Destroy(branch);
            }
        }
    }
}
