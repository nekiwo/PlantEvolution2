using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantClass;
using StatsGridClass;

public class Main : MonoBehaviour
{
    // Plant Config
    public static readonly int PlantCount = 10;
    public static readonly int StartBranchCount = 4;
    public static readonly int RayCount = 150;
    public static readonly float RayRange = 40;
    public static readonly int MaxBounces = 25;

    // Evolution Config
    public static readonly int SelectCount = 5;
    public static readonly int CycleCount = 10;
    public static readonly float MutationValue = 0.10f;

    // Refs
    public GameObject Seed;
    public GameObject Sun;

    // Plant Data
    private List<PlantTree> plants = new List<PlantTree>();

    private void Start()
    {
        for (int i = 0; i < PlantCount; i++)
        {
            PlantTree plant = new PlantTree(Seed.transform.position, Random.Range(45f, 135f));
            plant.GenPlant();
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
             * 
             */

            plants.Sort(delegate (PlantTree p1, PlantTree p2)
            {
                return p1.Hits.CompareTo(p2.Hits);
            });

            plants.RemoveRange(SelectCount, PlantCount - SelectCount);

            for (int i = 0; i < PlantCount - SelectCount; i++)
            {
                plants.Add(plants[i % plants.Count]);
            }

            for (int p = 0; p < PlantCount; p++)
            {
                plants[p].Grid.Randomize();
            }
        }
    }
}