﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;
    public int gridX, gridY;
    public Node parent;
    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }

    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

}