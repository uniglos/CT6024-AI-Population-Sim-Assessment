using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorTree;
public class PBT_Chase : Predator_Node
{
    private Transform _transform;

    private float _speed;

    public PBT_Chase(Transform transform, GameObject gameObject)
    {
        _transform = transform;
        _speed = gameObject.GetComponent<Predator_BT>().speed;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("P_Chasing");
        Transform target = (Transform)GetData("resource");
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }
        if (Vector3.Distance(_transform.position, target.position) < 0.01f)
        {
            state = NodeState.SUCCESS;
            return state;
        }
            state = NodeState.RUNNING;
        return state;
    }
}
