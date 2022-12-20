using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorTree;

public class Predator_BT : BTree
{
    //Traits------
    public float speed = 3.0f;
    public float vision = 6.0f;
    //-------------------
    public int Hunger = 10;
    public int Thirst = 0;
    public int NeedToMate = 0;
    public int Tolerance = 100;

    public float NeedTimer = 0.0f;

    protected override Predator_Node SetupTree()
    {
        
        Predator_Node root = new BT_Selector(new List<Predator_Node>
        {
            //new BT_Sequence(new List<Predator_Node>
            //{ new PBT_Reproduce(transform,gameObject),
            //}),
            //Find Food action sequence
            new BT_Sequence(new List<Predator_Node>
            { new PBT_KillNEat(transform,gameObject),
            }),
            new BT_Sequence(new List<Predator_Node>
        {
            
            new PBT_FindFood(transform, gameObject),
            new PBT_Chase(transform, gameObject),
            
        }),
        new PBT_Wander(transform, gameObject),
            //Reproduce Action sequence
            /*If need to reproduce
             * Find other predator
             * If both need to reproduce, reproduce(instatiate new predators)
             * For genes/evolution, take stats like speed, vision range and 
             * increment them randomly by 1 up or down from their parents scores 
             */
            //Find Food action sequence
            /* If hungry
             * Check for prey in cone
             * Chase then kill/eat
             */
            //Find Water action sequence
            /*If thirsty
             * Check for water in cone
             * Go towards then Drink
             */
            //Wander Action
            
        }) ;
        return root;
    }
}
