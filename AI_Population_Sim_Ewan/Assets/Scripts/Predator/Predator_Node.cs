using System.Collections;
using System.Collections.Generic;

// Used this for generic Behaviour Tree
//https://youtu.be/aR6wt5BlE-E

namespace PredatorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Predator_Node
    {
        protected NodeState state;

        public Predator_Node parent;
        protected List<Predator_Node> children = new List<Predator_Node>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();
        public Predator_Node()
        {
            parent = null;
        }
        public Predator_Node(List<Predator_Node> children)
        {
            foreach (Predator_Node child in children)
                Attach(child);
        }
        private void Attach(Predator_Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;
            Predator_Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            Predator_Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}
