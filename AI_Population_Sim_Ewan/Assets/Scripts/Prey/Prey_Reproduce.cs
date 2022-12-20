using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey_Reproduce : Prey
{
    GameObject PreyPrefab;
    //Traits-------------------------------
    public int Tolerance = 100;

    public float MaxSpeed = 2f;
    public float MaxRand = 3f;
    public float MinRand = 0.5f;

    public int Vision = 2;
    public int Hearing = 10;

    Prey SelfGenes;
    Prey OtherGenes;

    Prey ChildGenes;

    //-------------------------------------
    public void Procreate(GameObject self, GameObject other)
    {
        SelfGenes = self.GetComponent<Prey>();
        OtherGenes = other.GetComponent<Prey>();
        //Mix genes
        MaxSpeed = Random.Range(SelfGenes.MaxSpeed,OtherGenes.MaxSpeed);
        MaxRand = Random.Range(SelfGenes.MaxRand,OtherGenes.MaxRand);
        MinRand = Random.Range(SelfGenes.MinRand,OtherGenes.MinRand);
        Hearing = Random.Range(SelfGenes.Hearing,OtherGenes.Hearing);
        Tolerance = Random.Range(SelfGenes.Tolerance,OtherGenes.Tolerance);

        GameObject prefabInstance = Instantiate(PreyPrefab);
        ChildGenes = prefabInstance.GetComponent<Prey>();

        ChildGenes.MaxSpeed = MaxSpeed;
        ChildGenes.MaxRand = MaxRand;
        ChildGenes.MinRand = MinRand;
        ChildGenes.Hearing = Hearing;
        ChildGenes.Tolerance = Tolerance;
        ChildGenes.MaxSpeed = MaxSpeed;
    }
}
