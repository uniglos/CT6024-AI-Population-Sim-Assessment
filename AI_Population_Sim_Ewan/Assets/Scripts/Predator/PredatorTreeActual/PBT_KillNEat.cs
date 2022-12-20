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
        if(Vector3.Distance(_transform.position,target.position)<=_transform.localScale.x *1.2f)
        { 
                Collider[] food = Physics.OverlapBox(_transform.position,
                                                        _transform.localScale * 1.2f);
                foreach (var r in food)
                {
                    if (r.TryGetComponent(out Prey i))
                    {
                    _gameObject.GetComponent<Predator_BT>().Hunger -= 50;
                        i.Die();
                    ClearData("resource");
                }
                    else if (r.TryGetComponent(out Resources w))
                    {
                        if (w.resourceType == resourceType.Water)
                        {
                        _gameObject.GetComponent<Predator_BT>().Thirst -= w.ResourceVal;
                            w.GetComponent<Resources>().Overlap();
                            ClearData("resource");
                        }
                    }
                }

            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
