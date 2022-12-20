using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingDetection : MonoBehaviour
{
    
    private Vector3 SeenPos;
    private Vector3 LastPos;

    Prey prey;
    // Start is called before the first frame update
    void Start()
    {
        prey = transform.GetComponentInParent<Prey>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Something is triggered");

        if (other.gameObject.TryGetComponent(out Resources resource))
        {
            Debug.Log("Resource Found");

            Debug.Log("Added " + resource.resourceType.ToString());
            LastPos = other.gameObject.transform.position;


            if (!prey.resourceList.Contains(resource))
            {
                prey.resourceList.Add(resource);
            }
        }
        if(other.gameObject.TryGetComponent(out Predator_BT predator))
        {
            Debug.Log("Danger");
            if(!prey.predators.Contains(predator))
            {
                prey.predators.Add(predator);
            }

        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Predator_BT predator))
        {
            if(prey.predators.Contains(predator))
            {
                prey.predators.Remove(predator);
            }
        }
    }
}
