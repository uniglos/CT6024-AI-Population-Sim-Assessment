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
        object f = GetData("resource");
        if (f == null)
        {
            Debug.Log("Target is Null");
            state = NodeState.FAILURE;
            return state;
        }
        if (target == null)
        {
            Debug.Log("Target is Null");
            state = NodeState.FAILURE;
            return state;
        }
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            if(target.position == new Vector3(0.0f,-1.0f,0.0f))
            {
                Debug.Log("Weird Bug Maybe");
                ClearData("resource");
                state = NodeState.FAILURE;
                return state;
            }
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, _speed * Time.deltaTime);
            _transform.LookAt(target.position);
            Debug.Log("Still Chasing");
        }
        if (Vector3.Distance(_transform.position, target.position) <= 0.1f)
        {
            Debug.Log("Finished Chasing");
            state = NodeState.SUCCESS;
            return state;
        }
            state = NodeState.RUNNING;
        return state;
    }
}
