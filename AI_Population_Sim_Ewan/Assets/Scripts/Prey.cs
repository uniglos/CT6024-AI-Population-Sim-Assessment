using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prey : MonoBehaviour
{
    private int Hunger = 0;
    private int Thirst = 0;
    private int Discontentment;

    private int MaxSpeed = 2;
    private int Vision = 2;
    private int Hearing = 2;

    [SerializeField]
    GameObject HearingSphere;

    
    // Start is called before the first frame update
    void Start()
    {
        HearingSphere.GetComponent<SphereCollider>().radius = Hearing;
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
        //if prey can hear predator, turn towards the noise to look
            //unless prey is already fleeing, in which case keep fleeing
        //if prey can see predator, run away
        //once prey can no longer hear or see a predator, resume previous behaviour
    }

    public void SeekFood()
    {
            Debug.Log("Looking for Food");
        //Wander code
        //Attempt to find food nodes within vision cone
        //Check to see if there are predators nearby
            //If there are run away: This may be solved if the fleeing code is implemented well
        //Otherwise approach food node to eat
        //Reset food meter/increase food meter depending on how much food there was
    }
    public void SeekWater()
    {
        Debug.Log("Seeking Water");
        //Wander code
        //Attempt to find water nodes within vision cone
        //Check to see if there are predators nearby
        //If there are run away: This may be solved if the fleeing code is implemented well
        //Otherwise approach water node to drink
        //Reset thirst meter/decrease thirst meter depending on how much water there was
    }
    public void Flee()
    {
        Debug.Log("Running Away");
        //if can see/hear predator
        //run away
    }
    public void Reproduce()
    {
        Debug.Log("Making Baby");
    }
}
