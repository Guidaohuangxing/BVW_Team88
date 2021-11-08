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
    public int lane = 0;
    public AnimationCurve curve;
    private float timer = 0;

    public void MoveToPlayer()
    {
        timer += Time.deltaTime;
        if (setLane)
        {
            //Vector3 dir = targetPosition - this.transform.position + new Vector3(0, curve.Evaluate(timer), 0);
            //print(dir);
            //transform.position = transform.position + dir.normalized * speed * Time.deltaTime;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, speed * Time.deltaTime);
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

    public bool MatchLane(int PlayerLane)
    {
        if(lane == PlayerLane)
        {
            return true;
        }
        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DamagePoint")
        {
            FindObjectOfType<GameManager>().GotDamage(damage);
            FindObjectOfType<SoundFXManager>().PlayHit();
            Destroy(this.gameObject, 0.2f);

        }
    }
}
