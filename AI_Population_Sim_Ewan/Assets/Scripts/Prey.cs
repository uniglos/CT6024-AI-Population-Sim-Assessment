using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    private int Hunger = 0;
    private int Thirst = 0;
    private int Discontentment;

    private int MaxSpeed;
    private int Vision;
    private int Hearing;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Discontentment = (Hunger*Hunger) + (Thirst*Thirst);
        if(Discontentment >= 20)
        {
            if(Thirst > Hunger)
            { 
                SeekWater(); 
            }
            else
            {
                SeekFood(); 
            }
            

        }
        Hunger += 1;
        Thirst += 1;
    }

    public void SeekFood()
    {
            Debug.Log("Looking for Food");
    }
    public void SeekWater()
    {
        Debug.Log("Seeking Water");
    }
    public void Flee()
    {
        Debug.Log("Running Away");
    }
    public void Reproduce()
    {
        Debug.Log("Making Baby");
    }
}
