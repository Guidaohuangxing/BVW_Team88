using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public int LaneNum;
    public Transform startPos;
    public Transform endPos;
    public float objectSpeed;//reset the objects speed base on their lane not neccessary right now
    public Player belongPlayer;//this lane belong to which player;
}
