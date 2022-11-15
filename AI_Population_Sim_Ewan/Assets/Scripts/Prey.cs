using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=rQG9aUWarwE
public class Prey : MonoBehaviour
{
    private int Hunger = 0;
    private int Thirst = 0;
    private int Discontentment;

    private float MaxSpeed = 0.2f;
    private int Vision = 2;
    private int Hearing = 2;

    private Vector3 MovePosition;
    private Vector3 CurrentPos;

    private float Bounds = 10.0f;

    [SerializeField]
    GameObject HearingSphere;

    [SerializeField]
    private HashSet<GameObject> LocationofResources;

    
    // Start is called before the first frame update
    void Start()
    {
        HearingSphere.GetComponent<SphereCollider>().radius = Hearing;
        CurrentPos = gameObject.transform.position;
        MovePosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x - (MovePosition.x + CurrentPos.x) < 1.0f && 
            gameObject.transform.position.z - (MovePosition.z + CurrentPos.z) < 1.0f)
        {
            Debug.Log("Changed Direction");
            MovePosition = PickRandomPoint();
            CurrentPos = gameObject.transform.position;;
            if(CurrentPos.x + MovePosition.x > Bounds || CurrentPos.z + MovePosition.z > Bounds
                || CurrentPos.x + MovePosition.x < -Bounds || CurrentPos.z + MovePosition.z < -Bounds)
            {
                Debug.Log("Outside Range");
                MovePosition = new Vector3(0f, 0f, 0f);
            }
        }
        gameObject.transform.position +=  (MovePosition *  Time.deltaTime) * MaxSpeed;
        

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
        foreach (GameObject i in LocationofResources)
        {
            if(i.gameObject.GetComponent<Resources>() != null 
                && i.gameObject.GetComponent<Resources>().IsFood == true)
            {
                MovePosition = i.gameObject.transform.position;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Resources>() != null)
        {
            Debug.Log("Added Resource");
            LocationofResources.Add(other.gameObject);
        }
    }
}
