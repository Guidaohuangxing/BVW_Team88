using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDecisionBar : MonoBehaviour
{
    public bool isIntheRightArea = false;
    public Transform Target1Pos, Target2Pos;
    public float speed = 2f;
    public float threshold = 0.1f;
    private Vector3 targetPoint;
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
        MoveItSelf(targetPoint);
    }

    private void MoveItSelf(Vector3 Pos)
    {
        Vector3 dir = Pos - this.transform.position;
        dir = dir.normalized;
        this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + dir * speed);
    }
}
