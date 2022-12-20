using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resourceType {
    Food, 
    Water, 
    Mate
};

public class Resources : MonoBehaviour
{
    public Spawning SpawnPlane;
    public List<Prey> preyList;
    public resourceType resourceType;
 
    public int ResourceVal = 10;

    public void Start()
    {
        SpawnPlane = GameObject.Find("Spawn Plane").GetComponent<Spawning>();
    }
    public void Overlap()
    {
        //preyList = SpawnPlane.preyList;
        //foreach (var p in SpawnPlane.preyList)
        //{
        //    if (p.GetComponent<Prey>().resourceList.Contains(this))
        //        p.GetComponent<Prey>().resourceList.Remove(this);
        //}
        Destroy(gameObject);
        
    }

    public void OnDestroy()
    {
        //foreach (var p in SpawnPlane.preyList)
        //{
        //    if (p.GetComponent<Prey>().resourceList.Contains(this))
        //        p.GetComponent<Prey>().resourceList.Remove(this);
        //}
    }

}
