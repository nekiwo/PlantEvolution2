using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Config
    public static readonly int RayCount = 150;
    public static readonly float RayRange = 40;
    public static readonly int MaxBounces = 25;

    public GameObject seed;
    public PlantController.PlantTree plant;

    private void Start()
    {
        plant = new PlantController.PlantTree(seed.transform.position, 100);

        plant.root.branches.Add(new PlantController.Branch(plant.root.end, 75));
        plant.root.branches.Add(new PlantController.Branch(plant.root.end, 115));

        seed.GetComponent<PlantController>().RenderPlant(plant);
    }
}