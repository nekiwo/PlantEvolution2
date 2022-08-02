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
    public static readonly int PlantCount = 250;
    public static readonly int StartBranchCount = 4;
    public static readonly int RayCount = 180;
    public static readonly float RayRange = 100;
    public static readonly int MaxBounces = 15;

    // Evolution Config
    public static readonly int SelectCount = 100;
    public static readonly int CycleCount = 1000;
    public static readonly float MutationValue = 0.03f;
    public static readonly float BranchCost = 55;

    // Refs
    public GameObject Seed;
    public GameObject Sun;

    // Plant Data
    private List<PlantTree> plants = new List<PlantTree>();
    private List<int> maxBranches = new List<int>();
    private List<float> avgBranches = new List<float>();

    private void Start()
    {
        for (int i = 0; i < PlantCount; i++)
        {
            PlantTree plant = new PlantTree(Seed.transform.position, Random.Range(45f, 135f));
            plant.Hits = StartBranchCount * BranchCost;
            plants.Add(plant);
        }

        StartCoroutine(startSimulation());
    }

    private IEnumerator startSimulation()
    {
        for (int c = 0; c < CycleCount; c++)
        {
            for (int p = 0; p < PlantCount; p++)
            {
                plants[p].GenPlant((int)Mathf.Round(plants[p].Hits / BranchCost));
                Seed.GetComponent<PlantController>().RenderPlant(plants[p]);
                yield return 0;
                plants[p].Hits = Sun.GetComponent<SunController>().RayTrace();
                Seed.GetComponent<PlantController>().RemovePlant();
            }

            /*
             * Todo:
             * 
             * * sort array by hits
             * * remove bottom X# plants
             * * dublictate top plants until PlantCount is reached
             * * randomize all tiles of plant grids by X%
             * * add branches depending on hits
             * 
             */

            plants = plants.OrderBy(p => p.Hits).ToList();
            plants.Reverse();

            /*string test = "";
            for (int p = 0; p < PlantCount; p++)
            {
                test = test + plants[p].Hits.ToString() + ", ";
            }
            Debug.Log(test);*/

            maxBranches.Add((int)Mathf.Round(plants[0].Hits / BranchCost));
            avgBranches.Add((float)plants.Select(p => p.Hits / BranchCost).Average());
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