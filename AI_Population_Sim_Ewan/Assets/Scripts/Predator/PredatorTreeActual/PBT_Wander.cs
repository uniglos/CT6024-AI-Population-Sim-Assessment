using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PredatorTree;

public class PBT_Wander : Predator_Node
{
    private Transform _transform;

    private float _speed;
    float timer = 2.0f;
    public PBT_Wander(Transform transform, GameObject gameObject)
    {
        _transform = transform;
        _speed = gameObject.GetComponent<Predator_BT>().speed;
    }
    public override NodeState Evaluate()
    {
        Vector3 target = _transform.position + new Vector3(Mathf.Sin(_transform.rotation.y) * 2.0f,0.0f,Mathf.Cos(_transform.rotation.y) * 2.0f);

        //_transform.LookAt(target);
        

        if (timer > Random.Range(2.0f, 0.5f))
        {
            _transform.Rotate(0, _transform.rotation.y + Random.Range(-90.0f, 90.0f), 0);
            timer = 0.0f;
        }
        timer += Time.deltaTime;
        _transform.position += (_transform.forward * _speed * Time.deltaTime);
        state = NodeState.RUNNING;
        return state;
    }
}
