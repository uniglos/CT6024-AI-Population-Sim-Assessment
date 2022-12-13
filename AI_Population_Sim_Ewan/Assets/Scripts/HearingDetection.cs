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

            Debug.Log("Added " + resource.resource.ToString());
            LastPos = other.gameObject.transform.position;


            if (!prey.resource.Contains(resource))
            {
                prey.resource.Add(resource);
            }
        }
               
        
    }
}
