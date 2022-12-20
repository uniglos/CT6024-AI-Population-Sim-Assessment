using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator_Needs : MonoBehaviour
{
    public Predator_BT Predator_BT;
    public float NeedTimer;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Predator_BT.Hunger < 0) Predator_BT.Hunger = 0;
        if (Predator_BT.Thirst < 0) Predator_BT.Thirst = 0;
        NeedTimer -= Time.deltaTime;
        if (NeedTimer <= 0.0f)
        {
            Predator_BT.Hunger += 1;
            Predator_BT.Thirst += 1;
            NeedTimer = 6 - Predator_BT.speed;
        }
    }
}
