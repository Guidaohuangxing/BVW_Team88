using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour,Attackable
{

    public int scoreAward = 3;
    public float speed = 2;
    public int damage = 5;
    public Vector3 targetPosition;

    private void Update()
    {
        MoveToPlayer();
    }


    public void MoveToPlayer()
    {
        Vector3 dir = targetPosition - this.transform.position;
        transform.position = transform.position + dir.normalized * speed * Time.deltaTime;
    }

    public void onHit()
    {
        Destroy(this.gameObject, 1f);
    }
}
