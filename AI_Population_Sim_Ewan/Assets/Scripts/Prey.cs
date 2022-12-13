using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=rQG9aUWarwE
public class Prey : MonoBehaviour
{
    [SerializeField]
    private int Hunger = 0;
    [SerializeField]
    private int Thirst = 0;
    private int Discontentment;

    private float MaxSpeed = 2f;
    private int Vision = 2;
    private int Hearing = 20;

    private bool EatArea = false;
    Collider Collider;
    RaycastHit hitInfo;

    private bool DontWander = false;
    private bool TryToConsume = false;

    private Vector3 MovePosition;
    private Vector3 CurrentPos;

    [SerializeField]
    [Range(1,20)]
    private float Bounds = 10.0f;

    [SerializeField]
    GameObject HearingSphere;

    [SerializeField]
    Collider MainBody;

    [SerializeField]
    private Vector3 LastFoodSeen;
    public Vector3 FoodLastSeen
    {
        get { return LastFoodSeen; }
        set { LastFoodSeen = value; }
    }
    [SerializeField]
    private Vector3 LastWaterSeen;
    public Vector3 WaterLastSeen
    {
        get { return LastWaterSeen; }
        set { LastWaterSeen = value; }
    }
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
               // Debug.Log("Changed Direction");
                MovePosition = PickRandomPoint();
                CurrentPos = gameObject.transform.position; ;
                if (CurrentPos.x + MovePosition.x > Bounds || CurrentPos.z + MovePosition.z > Bounds
                    || CurrentPos.x + MovePosition.x < -Bounds || CurrentPos.z + MovePosition.z < -Bounds)
                {
                   // Debug.Log("Outside Range");
                    MovePosition = PickRandomPoint();
                    
                }
            }
        }
        gameObject.transform.position =  Vector3.MoveTowards(transform.position,MovePosition, MaxSpeed *  Time.deltaTime);
        gameObject.transform.LookAt(MovePosition);

        //Debug.Log(gameObject.transform.position + ","+ CurrentPos);
        //Debug.Log(MovePosition);

        

        Discontentment = (Hunger*Hunger) + (Thirst*Thirst);
        if(Discontentment >= 20)
        {
            //I believe this is still a state machine
            if(Thirst > Hunger)
            {
                //Add SeekWater to a list of actions
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
        if(LastFoodSeen != new Vector3(0.0f,0.0f,0.0f))
        {
            Debug.Log("Moving To Food");
            MovePosition = LastFoodSeen;
            DontWander = true;
        }// if (transform.position == LastFoodSeen)
        if (transform.position.x - LastFoodSeen.x < 1.5f
            && transform.position.z - LastFoodSeen.z < 1.5f)
        {
            Debug.Log("Arrived");
            Consume(LastFoodSeen);
            DontWander = false;
        }
              
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
        if (LastWaterSeen != new Vector3(0.0f, 0.0f, 0.0f))
        {
            Debug.Log("Moving To Food");
            MovePosition = LastWaterSeen;
            DontWander = true;
        }// if (transform.position == LastFoodSeen)
        if (transform.position.x - LastWaterSeen.x < 1.5f
            && transform.position.z - LastWaterSeen.z < 1.5f)
        {
            Debug.Log("Arrived");
            Consume(LastWaterSeen);
            DontWander = false;
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

    public void Consume(Vector3 Resource)
    {
        if(transform.position.x - Resource.x  < 10.0f 
            && transform.position.z - Resource.z < 10.0f)
        {
            Debug.Log("In Range");
            EatArea = Physics.BoxCast(transform.position, transform.localScale,
                                        transform.forward, out /*RaycastHit*/ hitInfo, new Quaternion(0f,0f,0f,0f),0.1f, LayerMask.GetMask("Resources")  ,QueryTriggerInteraction.Collide);
            if (EatArea)
            {
                Debug.Log("Eat attempted");
                if (hitInfo.transform.GetComponent<Resources>().IsFood)
                {
                    Hunger = Hunger - hitInfo.transform.GetComponent<Resources>().FoodVal;
                    Destroy(hitInfo.transform.gameObject);
                    LastFoodSeen = new Vector3(0.0f, 0.0f, 0.0f);
                }
                if (hitInfo.transform.GetComponent<Resources>().IsWater)
                {
                    Thirst = Thirst - hitInfo.transform.GetComponent<Resources>().WaterVal;
                    Destroy(hitInfo.transform.gameObject);
                    LastWaterSeen = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                DontWander = false;
                //LastFoodSeen = new Vector3(0.0f, 0.0f, 0.0f);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something is triggered");
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (EatArea)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * hitInfo.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * hitInfo.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * 3.1f);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * 3.1f, transform.localScale * 5f);
        }
    }

}
