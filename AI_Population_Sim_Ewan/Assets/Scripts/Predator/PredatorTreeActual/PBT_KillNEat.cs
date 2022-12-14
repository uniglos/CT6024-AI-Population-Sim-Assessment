using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorTree;

public class PBT_KillNEat : Predator_Node
{
    Transform _transform;

    GameObject _gameObject;

    public PBT_KillNEat(Transform transform, GameObject gameObject)
    {
        _transform = transform;

        _gameObject = gameObject;
    }

    public override NodeState Evaluate()
    {
        object f = GetData("resource");
        if (f == null)
        {
            Debug.Log("F is Null");
            state = NodeState.FAILURE;
            return state;
        }
        Transform target = (Transform)f;
        if (target == null)
        {
            Debug.Log("Target is Null");
            state = NodeState.FAILURE;
            return state;
        }
        if (Vector3.Distance(_transform.position,target.position)<= 0.01f)
        { 
            //Make sure we actually got something
                Collider[] food = Physics.OverlapSphere(_transform.position,
                                                        _transform.localScale.magnitude * 1.2f,LayerMask.GetMask("Prey"));
                foreach (var r in food)
                {
                    if (r.TryGetComponent(out Prey i))
                    {
                    //If Prey, eat and destroy prey gameobject
                        _gameObject.GetComponent<Predator_BT>().Hunger -= 50;
                        i.Die();
                        ClearData("resource");
                    
                    }
                    if (r.TryGetComponent(out Resources w))
                    {
                        if (w.resourceType == resourceType.Water)
                        {
                        //If Water, drink and destroy water gameobject
                        _gameObject.GetComponent<Predator_BT>().Thirst -= w.ResourceVal;
                        w.Overlap();
                        ClearData("resource");
                            
                        }
                    }
                }

            state = NodeState.SUCCESS;
            return state;
        }
        //anything else and the sequence fails
        state = NodeState.FAILURE;
        return state;
    }
}
