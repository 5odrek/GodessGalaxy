using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    private Vector3 worldPosition;
    private bool walkable;

    public Node(Vector3 _worldPosition, bool _walkable)
    {
        worldPosition = _worldPosition;
        walkable = _walkable;
    }

    public Vector3 GetNodeWorldPoint()
    {
        return worldPosition;
    }

    public bool GetNodeState()
    {
        return walkable;
    }

    public void SwitchNodeState()
    {
        walkable = !walkable;
    }
}
