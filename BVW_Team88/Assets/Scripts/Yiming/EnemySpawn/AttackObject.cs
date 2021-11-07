using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour,Attackable
{
    public string tagName;
    public bool SpecificAttack = false; 
    public int scoreAward = 3;
    public float speed = 2;
    public int damage = 5;
    public Vector3 targetPosition;
    public bool attackable = true;
    public bool setLane = false;

    public void MoveToPlayer()
    {
        if (setLane)
        {
            Vector3 dir = targetPosition - this.transform.position;
            transform.position = transform.position + dir.normalized * speed * Time.deltaTime;
        }
        else
        {
            Debug.Log("need set lane");
        }
    }

    public void onHit()
    {
        
    }

    public bool MatchType(string playerType)
    {
        //dont need to match
        if (!SpecificAttack)
        {
            return true;
        }
        else
        {
            if(tagName == playerType) { return true; }
        }
        return false;
    }
}
