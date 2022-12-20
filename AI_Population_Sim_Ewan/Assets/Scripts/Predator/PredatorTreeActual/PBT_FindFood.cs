using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorTree;
public class PBT_FindFood : Predator_Node
{
    private Transform _transform;

    private float _vision;
    private int _hunger;
    private int _thirst;
    private int _tolerance;

    public PBT_FindFood(Transform transform, GameObject _gameobject)
    {
        _transform = transform;
        if (_vision == 0.0f)
        {
            _vision = _gameobject.GetComponent<Predator_BT>().vision;
        }
        _hunger = _gameobject.GetComponent<Predator_BT>().Hunger;
        _thirst = _gameobject.GetComponent<Predator_BT>().Thirst;
        _tolerance = _gameobject.GetComponent<Predator_BT>().Tolerance;
    }
    public override NodeState Evaluate()
    {
        object f = GetData("resource");
        if(f == null)
        {
            if(_hunger > _tolerance / 10 || _thirst > _tolerance / 10)
            {
                Debug.Log("Looking");
                Collider[] food = Physics.OverlapBox(_transform.position,
                                                        _transform.localScale * _vision);
                foreach (var r in food)
                {
                    if (r.TryGetComponent(out Prey i))
                    {
                        Debug.Log("Found Prey");
                        parent.parent.SetData("resource", food[0].transform);
                    }
                    else if (r.TryGetComponent(out Resources w))
                    {
                        if (w.resourceType == resourceType.Water)
                        {
                            Debug.Log("Found Water");
                            parent.parent.SetData("resource", food[0].transform);
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
