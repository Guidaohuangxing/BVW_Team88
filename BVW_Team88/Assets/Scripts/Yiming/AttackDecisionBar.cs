using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDecisionBar : MonoBehaviour
{
    public Boss itsBoss;
    public bool isIntheRightArea = false;
    public Transform Target1Pos, Target2Pos;
    public float speed = 2f;
    public float threshold = 0.1f;
    public bool canMove = true;
    private Vector3 targetPoint;
    public GameObject destroyThisObject;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "RightArea")
        {
            isIntheRightArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RightArea")
        {
            isIntheRightArea = false;
        }
    }


    public void Start()
    {
        canMove = true;
        itsBoss = FindObjectOfType<Boss>();
        this.transform.position = new Vector3((Target1Pos.position + Target2Pos.position).x * Random.Range(0.1f, 0.9f), Target2Pos.position.y, Target2Pos.position.z);
        targetPoint = Target1Pos.position;
    }

    private void Update()
    {
      if(Vector3.Distance(this.transform.position, Target1Pos.position) < threshold)
        {
            targetPoint = Target2Pos.position;
        }  
      else if(Vector3.Distance(this.transform.position, Target2Pos.position) < threshold)
        {
            targetPoint = Target1Pos.position;
        }
        if (canMove)
        {
            MoveItSelf(targetPoint);
        }
        
    }

    private void MoveItSelf(Vector3 Pos)
    {
        Vector3 dir = Pos - this.transform.position;
        dir = dir.normalized;
        this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + dir * speed);
    }

    public void BeSlashedAndStop()
    {
        canMove = false;
        DestoryAllBar();
    }

    public void DestoryAllBar()
    {
        Destroy(destroyThisObject, 2f);
    }
}
