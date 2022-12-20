using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PredatorTree
{
    public abstract class BTree : MonoBehaviour
    {
        private Predator_Node root = null;

        protected void Start()
        {
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }

        protected abstract Predator_Node SetupTree();
    }
}
