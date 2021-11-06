using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMovement : MonoBehaviour
{
    public float speed = 2.5f;
    public Vector3 goToLoc;

    public bool laneSet = false;
    public bool collectable = false;
    public bool inPerfectZone = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (laneSet) { this.transform.position = Vector3.Lerp(this.transform.position, goToLoc, speed * Time.deltaTime); }
        
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "CollectionBoundary") {
            collectable = true;
        }
        if(col.gameObject.tag == "PerfectZone")
        {
            inPerfectZone = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PerfectZone")
        {
            inPerfectZone = false;
        }
    }

    public bool GetCollectable() {
        return collectable;
    }

    public int GetPoints()
    {
        if(inPerfectZone)
        {
            return 10;
        }
        else
        {
            return 5;
        }
    }

    public void SetLane(int lane) {
        if (lane == 1) {
            laneSet = true;
            goToLoc = GameObject.FindWithTag("L1EndPoint").transform.position;
            goToLoc.y = this.transform.position.y;
        }
        else if (lane == 2) {
            laneSet = true;
            goToLoc = GameObject.FindWithTag("L2EndPoint").transform.position;
            goToLoc.y = this.transform.position.y;
        }
    }
}
