using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int data;
    public Node parent;
    public List<Node> children = new List<Node>();
    public Node(int data)
    {
        //Debug.Log(" A Node is made with data = " + data);
        this.data = data;
        //Node node = this;
        //node.parent = null;
        //node.data = data;
        //node.children = null;
    }
    public void add_child(Node new_node)
    {
        new_node.parent = this;
        this.children.Add(new_node);
    }

    public void parent_jump(int index)
    {
        Node kept = this.children[index];
        foreach (Node child in kept.children)
        {
            child.parent = this;
        }
    }
    public void Print_tree()
    {
        Debug.Log("This is the root " + this.data);
        foreach(Node child in this.children)
        {
            Debug.Log("This is the first child" + child.data);
            foreach(Node secondChild in child.children)
            {
                Debug.Log("This is the second degree child " + secondChild.data);
                foreach(Node thirdchild in secondChild.children)
                {
                    Debug.Log(" This is the third child " + thirdchild.data);
                }
            }
        }
    }
}
