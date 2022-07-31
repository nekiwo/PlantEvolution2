using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantClass;
using StatsGridClass;

public class Main : MonoBehaviour
{
    // Config
    public static readonly int RayCount = 150;
    public static readonly float RayRange = 40;
    public static readonly int MaxBounces = 25;
    public static readonly int StartBranchCount = 4;

    // Refs
    public GameObject Seed;
    public GameObject Sun;

    // Plant data
    private PlantTree plant;

    private void Awake()
    {
        plant = new PlantTree(Seed.transform.position, Random.Range(45f, 135f));
        plant.GenPlant();
        
        Seed.GetComponent<PlantController>().RenderPlant(plant);
    }

    private bool sunStarted = false;
    private void Update()
    {
        if (!sunStarted) 
        {
            sunStarted = true;
            Sun.GetComponent<SunController>().RayTrace();
        }
    }
}