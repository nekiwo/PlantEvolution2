using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantClass;
using BranchClass;
using StatsGridClass;
using System.Linq;

public class Main : MonoBehaviour
{
    // Plant Config
    public static readonly int PlantCount = 50;
    public static readonly int RayCount = 180;
    public static readonly float RayRange = 100;
    public static readonly int MaxBounces = 15;

    // Evolution Config
    public static readonly int SelectCount = 15;
    public static readonly int CycleCount = 1000;
    public static readonly float MutationValue = 0.03f;
    public static readonly float BranchCost = 90;

    // Refs
    public GameObject Seed;
    public GameObject Sun;

    // Plant Data
    private List<PlantTree> plants = new List<PlantTree>();
    private List<int> maxBranches = new List<int>();
    private List<float> avgBranches = new List<float>();

    private void Start()
    {
        for (int p = 0; p < PlantCount; p++)
        {
            PlantTree plant = new PlantTree(Seed.transform.position, Random.Range(45f, 135f));
            plants.Add(plant);
        }

        StartCoroutine(startSim());
    }

    private IEnumerator startSim()
    {
        for (int c = 0; c < CycleCount; c++)
        {
            for (int p = 0; p < PlantCount; p++)
            {
                plants[p].Root.Branches.Clear();
                plants[p].BranchCount = 1;
                List<Branch> tips = new List<Branch>();
                tips.Add(plants[p].Root);
                Seed.GetComponent<PlantController>().RenderPlant(plants[p].Root, false);

                yield return genBranch();
                IEnumerator genBranch()
                {
                    Branch newBranch = plants[p].GenBranch(tips);

                    Seed.GetComponent<PlantController>().RenderPlant(newBranch, true);
                    yield return new WaitForSeconds(0.05f);
                    plants[p].Hits = Sun.GetComponent<SunController>().RayTrace();
                    if (plants[p].Hits == 0)
                    {
                        Debug.Log(plants[p].Hits.ToString() + " / " + Main.BranchCost.ToString() + " = " + (plants[p].Hits / Main.BranchCost).ToString() + " >= " + plants[p].BranchCount);

                        yield return new WaitForSeconds(2);
                    }

                    plants[p].BranchCount++;

                    if (plants[p].Hits / Main.BranchCost >= plants[p].BranchCount)
                    {
                        yield return genBranch();
                        Debug.Log("lived");
                    }
                    else
                    {
                        Debug.Log("died");
                    }
                }

                Seed.GetComponent<PlantController>().RemovePlant();
            }

            plants = plants.OrderBy(p => p.BranchCount).ToList();
            plants.Reverse();

            maxBranches.Add(plants[0].BranchCount);
            avgBranches.Add((float)plants.Select(p => p.BranchCount).Average());
            if (maxBranches.Count > 1)
            {
                Debug.DrawLine(
                    new Vector2(c + 7, maxBranches[maxBranches.Count - 1] - 10),
                    new Vector2(c + 6, maxBranches[maxBranches.Count - 2] - 10),
                    Color.blue,
                    1000000
                );

                Debug.DrawLine(
                    new Vector2(c + 7, avgBranches[avgBranches.Count - 1] - 10),
                    new Vector2(c + 6, avgBranches[avgBranches.Count - 2] - 10),
                    Color.green,
                    1000000
                );
            }

            plants.RemoveRange(SelectCount, PlantCount - SelectCount);

            for (int i = 0; i < PlantCount - SelectCount; i++)
            {
                plants.Add(plants[i % plants.Count].Dublicate());
            }

            for (int p = 0; p < PlantCount; p++)
            {
                plants[p].Grid.Randomize();
            }
        }
    }
}