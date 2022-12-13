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
    public resourceType resource;
 
    public int ResourceVal = 10;

}
