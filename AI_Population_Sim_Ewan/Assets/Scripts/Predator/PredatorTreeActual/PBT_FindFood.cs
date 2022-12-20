using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorTree;
public class PBT_FindFood : Predator_Node
{
    private Transform _transform;
    Predator_BT Predator;

    private float _vision;
    private int _hunger;
    private int _thirst;
    private int _tolerance;

    public PBT_FindFood(Transform transform, GameObject _gameobject)
    {
        _transform = transform;
        
        Predator = _gameobject.GetComponent<Predator_BT>();
        
    }
    public override NodeState Evaluate()
    {
        object f = GetData("resource");
       // Debug.Log("Here");
        if(f == null)
        {
            //Debug.Log("Also Here");
            if (Predator.Hunger > Predator.Tolerance / 10 || Predator.Thirst > Predator.Tolerance / 10)
            {
               // Debug.Log("Looking");
                Collider[] food = Physics.OverlapSphere(_transform.position,
                                                        _transform.localScale.magnitude * Predator.vision,LayerMask.GetMask("Prey"));
                foreach (var r in food)
                {
                    if (r.TryGetComponent(out Prey i))
                    {
                        //Debug.Log("Found Prey");
                        parent.parent.SetData("resource", food[0].transform);
                        state = NodeState.SUCCESS;
                        return state;
                    }
                    else if (r.TryGetComponent(out Resources w))
                    {
                        if (w.resourceType == resourceType.Water)
                        {
                           // Debug.Log("Found Water");
                            parent.parent.SetData("resource", food[0].transform);
                            state = NodeState.SUCCESS;
                            return state;
                        }
                        if (w.resourceType == resourceType.Food)
                        {
                            state = NodeState.FAILURE;
                            return state;
                        }
                    }
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
