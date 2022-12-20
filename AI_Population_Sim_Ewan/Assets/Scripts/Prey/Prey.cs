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
    [SerializeField]
    private int NeedToMate = 0;
    private int Discontentment;
    private float timer = 3;

    //Traits-------------------------------
    private int Tolerance = 100;

    private float MaxSpeed = 2f;
    private float MaxRand = 3f;
    private float MinRand = 0.5f;

    private int Vision = 2;
    private int Hearing = 10;
    
    //-------------------------------------
    private bool EatArea = false;
    Collider Collider;
    RaycastHit hitInfo;

    private bool DontWander = false;
    private bool TryToConsume = false;

    private Vector3 MovePosition;
    private Vector3 CurrentPos;

    [SerializeField]
    [Range(1, 100)]
    private float Bounds = 50.0f;

    [SerializeField]
    GameObject HearingSphere;

    [SerializeField]
    Collider MainBody;

    //List of detected resources
    public List<Resources> resourceList;

    //List of detected predators
    public List<Predator_BT> predators;

    //List of detected prey
    public List<GameObject> preyList;

    private float NeedTimer = 5;

    //Debug Tool
    public bool DisplayPos = false;



    // Start is called before the first frame update
    void Start()
    {
        HearingSphere.GetComponent<SphereCollider>().radius = Hearing;
        CurrentPos = gameObject.transform.position;
        MovePosition = new Vector3(0.0f, 0.0f, 0.0f);
        
    }

    int calculateScore(Resources i)
    {
        //Parameters for best score
        //Distance, resourceVal, Prey Priorities
        int score = 0; //Bigger is better
        float Distance = Vector3.Distance(transform.position, i.transform.position);
        int Value = i.ResourceVal;
        
        //Priorities, a ratio of how much I need this particular resource
        int ThirstNeed = Tolerance - Thirst;
        int HungerNeed = Tolerance - Hunger;

        


        if (i.resourceType == resourceType.Food && ThirstNeed > HungerNeed)
        {
            Value *= 20;
        }
        else if (i.resourceType == resourceType.Water && ThirstNeed < HungerNeed)
        {
            Value *= 20;
        }
        score = Value - Mathf.RoundToInt(Distance);
        // Hunger - Thirst, if negative need water, if positive need food,
        // if an extreme number, really need the extreme number's resource
        // otherwise not a priority
        foreach (var p in predators)
        {
            if (Vector3.Distance(p.transform.position, i.transform.position) < 5.0f
                || Vector3.Distance(transform.position, p.transform.position) < 5.0f)
            { score -= 5; }
        }

        return score;
    }

    // Update is called once per frame
    void Update()
    {
        if (DisplayPos)
        { Debug.Log(MovePosition); }

        if (DontWander == false)
        {
            //Debug.Log("Wandering");
            if (timer > Random.Range(MaxRand, MinRand))
            {
                transform.Rotate(0, transform.rotation.y + Random.Range(-90.0f, 90.0f), 0);
                timer = 0.0f;
            }
            timer += Time.deltaTime;
            if (((transform.position.x + (transform.forward.x * MaxSpeed * Time.deltaTime)) <= 50.0f)
                && ((transform.position.x + (transform.forward.x * MaxSpeed * Time.deltaTime)) >= -50.0f)
                && ((transform.position.z + (transform.forward.z * MaxSpeed * Time.deltaTime)) <= 50.0f)
                && ((transform.position.z + (transform.forward.z * MaxSpeed * Time.deltaTime)) >= -50.0f))
            {
                transform.position += (transform.forward * MaxSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, transform.rotation.y + Random.Range(-90.0f, 90.0f), 0);
                timer = -3.0f;
            }
        }
        else
        { 
            gameObject.transform.position = Vector3.MoveTowards(transform.position, MovePosition, MaxSpeed * Time.deltaTime);
            gameObject.transform.LookAt(MovePosition);
            if(transform.position == MovePosition)
            {
                DontWander = false;
            }
        }
            

            Discontentment = (Hunger * Hunger) + (Thirst * Thirst);
        if (Hunger < 0) Hunger = 0;
        if (Thirst < 0) Thirst = 0;

        if (Discontentment >= Tolerance/4)
        {
            //Debug.Log("Require Sustenance");
            Resources bestAction = null;
            int bestScore = 0;
            foreach (Resources i in resourceList)
            {
                if(i == null)
                {
                    resourceList.Remove(i);
                    continue;
                }
                // what is best action?
                // FigureOutHowMuchAResourceWillDecreaseDiscontentment();
                if(calculateScore(i) > bestScore)
                {
                    bestScore = calculateScore(i);
                    if (NeedToMate <= bestScore)
                    {
                        bestAction = i;
                    }
                    else 
                    {
                        bestAction = null;
                        Debug.Log("nulled");
                    }
                }
            }

            if (bestAction != null) Seek(bestAction.gameObject);
            else { Reproduce(); }
            
        }

        if (predators != null)
        {
            foreach (var p in predators)
            {
                if (Vector3.Distance(p.transform.position, transform.position) < Hearing / 2)
                {
                    Flee(p);
                }
                else
                {
                    DontWander = false;

                }
            }
        }
        
        NeedTimer -= Time.deltaTime;
        if (NeedTimer <= 0.0f)
        {
            Hunger += 0;
            Thirst += 0;
            NeedToMate += 100;
            NeedTimer = 6 - MaxSpeed;
        }
        if(NeedToMate >= Tolerance)
        {
            Debug.Log("Conditions Met");
            Reproduce();
        }
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
    public void Seek(GameObject i)
    {
        //Debug.Log("Looking");
        if (i == null)
        {
            Debug.Log("Wrongo");
            return; // something bad happened
        }
        else
        {
            Debug.Log("Moving To ");
            MovePosition = i.transform.position;
            DontWander = true;
        }// if (transform.position == LastFoodSeen)
        if (Vector3.Distance(transform.position, i.transform.position) < 1.5f)
        {
            //Debug.Log("Arrived");
            if (i.GetComponent<Resources>())
            {
                Consume(i);
                DontWander = false;
            }
            if(i.GetComponent<Prey>())
            {
                Debug.Log("The Magic of procreation");
            }
        }
    }
    public void Flee(Predator_BT predator)
    {
        //Debug.Log("Running Away");
        DontWander = true;
        MovePosition = transform.position - (predator.transform.position - transform.position);
        
        //if can see/hear predator
        //run away
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Reproduce()
    {
        //DontWander = true;

        GameObject closestMate = null;
        Debug.Log("Afew");
        foreach (GameObject p in preyList)
        {
            if (p == null)
                this.preyList.Remove(p);

            if (p != this.gameObject)
            {
                if (closestMate == null)
                    closestMate = p;
                if (Vector3.Distance(transform.position, p.transform.position) <
                    (Vector3.Distance(transform.position, closestMate.transform.position)))
                {
                    closestMate = p;
                }
            }
        }
        if (closestMate != null)
        {
            Debug.Log("Not Null");
            Seek(closestMate);
        }
        MovePosition = closestMate.transform.position;
        Debug.Log("Making Baby");
    }

    public void Consume(GameObject Resource)
    {
        if (Vector3.Distance(transform.position, Resource.transform.position) < 1.5f)
        {
            //Debug.Log("In Range");
            Collider[] hitInfo = Physics.OverlapSphere(transform.position, 
                                                    transform.localScale.magnitude * 2);

            foreach (var c in hitInfo)
            {
                if (c.TryGetComponent(out Resources i))
                {
                    if (i.resourceType == resourceType.Food)
                    {
                        Hunger -= i.ResourceVal;

                    }
                    if (i.resourceType == resourceType.Water)
                    {
                        Thirst -= i.ResourceVal;

                    }

                    Destroy(i.gameObject);
                    resourceList.Remove(i);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Something is triggered");

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
            Gizmos.DrawRay(transform.position, transform.forward);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward, transform.localScale);
        }
    }

}
