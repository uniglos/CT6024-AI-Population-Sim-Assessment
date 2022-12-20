using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resourceType {
    Food, 
    Water, 
    Shelter
};

public class Resources : MonoBehaviour
{
    public resourceType resourceType;
 
    public int ResourceVal = 10;

    public void Overlap()
    {
        
            Destroy(this.gameObject);
        
    }

}
