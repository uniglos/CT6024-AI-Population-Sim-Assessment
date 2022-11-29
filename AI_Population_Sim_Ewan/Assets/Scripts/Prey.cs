using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=rQG9aUWarwE
public class Prey : MonoBehaviour
{
    private int Hunger = 0;
    private int Thirst = 0;
    private int Discontentment;

    private float MaxSpeed = 2f;
    private int Vision = 2;
    private int Hearing = 20;

    private bool DontWander = false;
    private bool TryToConsume = false;

    private Vector3 MovePosition;
    private Vector3 CurrentPos;

    private float Bounds = 10.0f;

    [SerializeField]
    GameObject HearingSphere;

    [SerializeField]
    Collider MainBody;

    [SerializeField]
    private GameObject LastFoodSeen;
    [SerializeField]
    private Vector3 LastWaterSeen;
    //Maybe make this an array ^, of like 3 and have a memory value that increases the size of the array

    //Debug Tool
    public bool DisplayPos = false;



    // Start is called before the first frame update
    void Start()
    {
        //MainBody = GameObject.GetComponent<SphereCollider>();
        HearingSphere.GetComponent<SphereCollider>().radius = Hearing;
        CurrentPos = gameObject.transform.position;
        MovePosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (DisplayPos)
        { Debug.Log(MovePosition); }

        if (DontWander == false)
        {
            if (gameObject.transform.position.x - (MovePosition.x ) < 1.0f &&
                gameObject.transform.position.z - (MovePosition.z ) < 1.0f)
            {
                Debug.Log("Changed Direction");
                MovePosition = PickRandomPoint();
                CurrentPos = gameObject.transform.position; ;
                if (CurrentPos.x + MovePosition.x > Bounds || CurrentPos.z + MovePosition.z > Bounds
                    || CurrentPos.x + MovePosition.x < -Bounds || CurrentPos.z + MovePosition.z < -Bounds)
                {
                    Debug.Log("Outside Range");
                    MovePosition = new Vector3(0f, 0f, 0f);
                }
            }
        }
        gameObject.transform.position =  Vector3.MoveTowards(transform.position,MovePosition, MaxSpeed *  Time.deltaTime);
        

        //Debug.Log(gameObject.transform.position + ","+ CurrentPos);
        //Debug.Log(MovePosition);

        

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

    IEnumerator Wander()
    {
        yield return new WaitForSeconds(.1f);
    }
    public Vector3 PickRandomPoint()
    {
        var point = Random.insideUnitSphere * Hearing;

        point.y = 0;
        
        return point;
    }
    public void SeekFood()
    {
        Debug.Log("Looking for Food");
        if(LastFoodSeen != null)
        {
            Debug.Log("Moving To Food");
            MovePosition = LastFoodSeen.transform.position;
        }
        if(transform.position == LastFoodSeen.transform.position)
        {
            Debug.Log("Arrived");
            Consume(LastFoodSeen);
            DontWander = false;
        }
       /* if (transform.position == MovePosition && LastFoodSeen != null)
        {
            Debug.Log("Arrived");
            DontWander = false;
            TryToConsume = true;
        }
        else
        {
            MovePosition = LastFoodSeen.transform.position;
            DontWander = true;
        }*/
        
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
        if (transform.position == MovePosition)
        {
            Debug.Log("Arrived");
            DontWander = false;
            TryToConsume = true;
        }
        else
        {
            MovePosition = LastWaterSeen;
            DontWander = true;
        }
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

    public void Consume(GameObject Resource)
    {
        if(transform.position.x - Resource.transform.position.x  < 1.0f 
            && transform.position.z - Resource.transform.position.z < 1.0f)
        {
            if(Resource.GetComponent<Resources>().IsFood)
            {
                Hunger = Hunger - Resource.GetComponent<Resources>().FoodVal;
                Destroy(Resource);
            }
            if (Resource.GetComponent<Resources>().IsWater)
            {
                Thirst = Thirst - Resource.GetComponent<Resources>().WaterVal;
                Destroy(Resource);
            }
            DontWander = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something is triggered");
        
    }

    private void OnTriggerStay(Collider other)
    {
        
        
        Debug.Log("Collided");
        if (other.gameObject.GetComponent<Resources>() != null)
        {
            Debug.Log("Resource Found");
            if (LastFoodSeen.transform.position != other.gameObject.transform.position
                && LastWaterSeen != other.gameObject.transform.position)
            {
                Debug.Log("Added Resource");
                if (other.gameObject.GetComponent<Resources>().IsFood == true)
                {
                    LastFoodSeen = other.gameObject;
                }
                else if (other.gameObject.GetComponent<Resources>().IsWater == true)
                {
                    LastWaterSeen = other.gameObject.transform.position;
                }
            }
        }/*
        if(other.gameObject.GetComponent<Resources>() != null
            && TryToConsume == true)
        {
            if(other.gameObject.GetComponent<Resources>().IsFood)
            { Hunger = 0; }
            if (other.gameObject.GetComponent<Resources>().IsWater)
            { Thirst = 0; }
            //Change these to be editable in the editor
            TryToConsume = false;
            DontWander = false;
            Destroy(other.gameObject);
        }*/
    }
}
