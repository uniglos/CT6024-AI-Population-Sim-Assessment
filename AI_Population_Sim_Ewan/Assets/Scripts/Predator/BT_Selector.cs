using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PredatorTree
{
    public class BT_Selector : Predator_Node
    {
        public BT_Selector() : base() { }
        public BT_Selector(List<Predator_Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Predator_Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
