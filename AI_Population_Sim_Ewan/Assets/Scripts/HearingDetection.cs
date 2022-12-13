using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingDetection : MonoBehaviour
{
    private Vector3 FoodSeenPos;
    private Vector3 LastFoodSeenPos;

    private Vector3 WaterSeenPos;
    private Vector3 LastWaterSeenPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Something is triggered");
        if (other.gameObject.GetComponent<Resources>() != null)
        {
            Debug.Log("Resource Found");
            if (LastFoodSeenPos != other.gameObject.transform.position
                && LastWaterSeenPos != other.gameObject.transform.position)
            {
                
                if (other.gameObject.GetComponent<Resources>().IsFood == true)
                {
                    Debug.Log("Added Food");
                    LastFoodSeenPos = other.gameObject.transform.position;
                    transform.GetComponentInParent<Prey>().FoodLastSeen = LastFoodSeenPos;
                }
                if (other.gameObject.GetComponent<Resources>().IsWater == true)
                {
                    Debug.Log("Water Added");
                    LastWaterSeenPos = other.gameObject.transform.position;
                    transform.GetComponentInParent<Prey>().WaterLastSeen = LastWaterSeenPos;
                }
            }
        }
        
        
    }
}
