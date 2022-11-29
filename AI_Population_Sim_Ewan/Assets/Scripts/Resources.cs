using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public bool IsWater = false;
    public bool IsFood = false;
    public GameObject ThisResource;

    public int FoodVal = 10;
    public int WaterVal = 10;

    // Start is called before the first frame update
    void Start()
    {
        ThisResource = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
       //Debug.Log("Something is triggered");
    }

}
