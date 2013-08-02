using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using J832.Common;
using System.IO;

namespace AnimatedGraph
{
    public class Node<T>
    {
        internal Node(T item, NodeCollection<T> parent)
        {
            Debug.Assert(item != null && parent != null);

            m_item = item;
            m_parent = parent;
        }

        public ReadOnlyObservableCollection<Node<T>> ChildNodes
        {
            get
            {
                if (m_children == null)
                {
                    m_parent.NodeChildrenChanged += m_parent_NodeChildrenChanged;

                    m_children = new ObservableCollection<Node<T>>(m_parent.GetChildren(this.m_item));
                    m_childrenReadOnly = new ReadOnlyObservableCollection<Node<T>>(m_children);
                }
                return m_childrenReadOnly;
            }
        }

        public T Item { get { return m_item; } }

        public override string ToString()
        {
            return m_item.ToString();
        }

        private void m_parent_NodeChildrenChanged(object sender, NodeChildrenChangedArgs<T> args)
        {
            if (args.Parent.Equals(this.m_item))
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    Debug.Assert(!m_children.Contains(m_parent[args.Child]));
                    m_children.Add(m_parent[args.Child]);
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    Debug.Assert(m_children.Contains(m_parent[args.Child]));
                    m_children.Remove(m_parent[args.Child]);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                Debug.Assert(args.Parent != null);
            }
        }

        private ObservableCollection<Node<T>> m_children;
        private ReadOnlyObservableCollection<Node<T>> m_childrenReadOnly;

        private readonly T m_item;
        private readonly NodeCollection<T> m_parent;
    }

    public class NodeChildrenChangedArgs<TNode> : EventArgs
    {
        public NodeChildrenChangedArgs(TNode parent, TNode child, NotifyCollectionChangedAction action)
        {
            Util.RequireNotNull(parent, "parent");
            Util.RequireNotNull(child, "child");

            if (!(action == NotifyCollectionChangedAction.Add || action == NotifyCollectionChangedAction.Remove))
            {
                throw new ArgumentException("Only supports Add and Remove", "action");
            }

            Parent = parent;
            Child = child;
            Action = action;
        }

        public TNode Parent { get; private set; }
        public TNode Child { get; private set; }
        public NotifyCollectionChangedAction Action { get; private set; }
    }

    public class NodeCollection<TNode>
    {
        private readonly Dictionary<TNode, Node<TNode>> nodesCollection;
        private readonly Dictionary<TNode, HashSet<TNode>> nodesConnections;

        //private readonly ObservableCollection<TNode> nodesValues;
        //private readonly ReadOnlyObservableCollection<TNode> m_nodesReadOnly;

        public NodeCollection(IEnumerable<TNode> nodes)
        {
            Util.RequireNotNull(nodes, "nodes");

            List<TNode> nodeList = nodes.ToList();

            Util.RequireArgument(
                nodeList.All(item => item != null),
                "nodes",
                "All items must be non-null.");

            //Util.RequireArgument(nodeList.AllUnique(), "nodes");

            nodesCollection = new Dictionary<TNode, Node<TNode>>();
            nodesConnections = new Dictionary<TNode, HashSet<TNode>>();

            //nodesValues = new ObservableCollection<TNode>(nodeList);
            //m_nodesReadOnly = new ReadOnlyObservableCollection<TNode>(nodesValues);

            foreach (TNode node in nodeList)
            {
                nodesConnections.Add(node, new HashSet<TNode>());
            }
        }

        //public ReadOnlyCollection<TNode> Nodes
        //{
        //    get
        //    {
        //        return m_nodesReadOnly;
        //    }
        //}

        public Node<TNode> this[TNode value]
        {
            get
            {
                if (!nodesCollection.ContainsKey(value))
                {
                    if (!nodesConnections.ContainsKey(value))
                    {
                        throw new ArgumentException("No node exists with the provided value.", "node");
                    }
                    else
                    {
                        nodesCollection[value] = new Node<TNode>(value, this);
                    }
                }
                return nodesCollection[value];
            }
        }

        #region Node operations

        private bool isContainsNode(TNode value)
        {
            foreach (Node<TNode> node in nodesCollection.Values)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                    if (node.ChildNodes[i].Item.Equals(value)) 
                        return true;
            }
            return false;
        }

        private bool isContainsKey(TNode key)
        {
            return nodesConnections.ContainsKey((TNode)key);
        }

        public void Add(TNode node)
        {
            //if (!isContainsNode(node))
           // {
                //nodesConnections.Add(node, new HashSet<TNode>());
           // }
            //if (nodesValues.Contains(node))
            //{
            //    throw new ArgumentException("Already have this value.", "node");
            //}
            //else
            //{
               
            //   // nodesValues.Add(node);
            //}
        }

        public bool Remove(TNode node)
        {
            if (isContainsKey(node))
            {
                //Debug.Assert(nodesValues.Contains(node));

                foreach (TNode value in nodesConnections[node])
                {
                    nodesConnections.Remove(value);
                }
                //nodesConnections.Values.ForEach(hashSet => hashSet.Remove(node));
                nodesConnections.Remove(node);

               // nodesValues.Remove(node);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddEdge(TNode node1, TNode node2)
        {
            if (node1.Equals(node2))
            {
                Debug.Assert(!nodesConnections[node1].Contains(node1));
                Debug.Assert(!nodesConnections[node2].Contains(node2));

                throw new ArgumentException("Cannot create an edge between the same node.");
            }
            else if (nodesConnections[node1].Contains(node2))
            {
                //Debug.Assert(nodesConnections[node2].Contains(node1));
                //throw new ArgumentException("This edge already exists.");
            }
            else
            {
                Debug.Assert(!nodesConnections[node2].Contains(node1));
               
                nodesConnections[node1].Add(node2);
                nodesConnections[node2].Add(node1);

                OnNodeChildrenChanged(
                    new NodeChildrenChangedArgs<TNode>(node1, node2, NotifyCollectionChangedAction.Add));
                OnNodeChildrenChanged(
                    new NodeChildrenChangedArgs<TNode>(node2, node1, NotifyCollectionChangedAction.Add));
            }
        }

        public void RemoveEdge(TNode node1, TNode node2)
        {
            if (node1.Equals(node2))
            {
                Debug.Assert(!nodesConnections[node1].Contains(node1));
                Debug.Assert(!nodesConnections[node2].Contains(node2));

                throw new ArgumentException("One cannot create an edge between the same node.");
            }
            else if (nodesConnections[node1].Contains(node2))
            {
                Debug.Assert(nodesConnections[node2].Contains(node1));

                nodesConnections[node1].Remove(node2);
                nodesConnections[node2].Remove(node1);

                OnNodeChildrenChanged(
                    new NodeChildrenChangedArgs<TNode>(node1, node2, NotifyCollectionChangedAction.Remove));
                OnNodeChildrenChanged(
                    new NodeChildrenChangedArgs<TNode>(node2, node1, NotifyCollectionChangedAction.Remove));
            }
            else
            {
                Debug.Assert(!nodesConnections[node2].Contains(node1));
                throw new ArgumentException("This edge does not exist");
            }
        }

        public void AddNode(TNode parentNode, TNode node)
        {
            if (isContainsKey(node))
            {
                throw new ArgumentException("This node is already exists");
            }
            else
            {
                nodesConnections.Add(node, new HashSet<TNode>());
                AddEdge(parentNode, node);
            }
        }

        public void RemoveNode(TNode parentNode, TNode node)
        {
            if (!nodesConnections.ContainsKey(node))
            {
                throw new ArgumentException();
            }
            else
            {
                RemoveEdge(parentNode, node);
                Remove(node);
            }
        }

        #endregion

        public event EventHandler<NodeChildrenChangedArgs<TNode>> NodeChildrenChanged;

        protected void OnNodeChildrenChanged(NodeChildrenChangedArgs<TNode> args)
        {
            EventHandler<NodeChildrenChangedArgs<TNode>> handler = NodeChildrenChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        internal List<Node<TNode>> GetChildren(TNode item)
        {
            Debug.Assert(nodesConnections.ContainsKey(item));

            return nodesConnections[item].Select(child => this[child]).ToList();
        }


    }
}
