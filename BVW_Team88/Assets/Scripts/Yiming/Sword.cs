using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //turn the vector3 from vive tracker to paths
    //vive tracker movement
    public Transform swordTracker;
    //sword position on which path
    public int position = 0;

    public float swordLength = 5f;
    public Vector3 currentPos;


    public Vector3 previousPos;
    //minmun speed to make slash;
    public float minSpeed = 30f;

    public bool slashing = false;

    public FakeEnemy fakeEnemy;

    public Vector3 slashOffset;

    private Slash currentSlash;
    private float slashTimer = 0;
    public int UpdateInterval = 20;
    public GameObject SlashPrefab;

    [System.Serializable]
    public class SwordArea
    {
        public int number;
        public float leftX;
        public float rightX;
        public float highY;
        public float lowY;
        public Vector3 swordPos;
        public bool InArea(Vector3 point)
        {
            if(point.x<= rightX && point.x>=leftX && point.y<=highY && point.y >= lowY)
            {
                return true;
            }
            return false;
        }
    }

    
    public List<SwordArea> swordAreas = new List<SwordArea>();
    private void Start()
    {
        Initialized();
    }
    private void Update()
    {
        currentPos = swordTracker.InverseTransformPoint(transform.position + transform.forward * swordLength);
        //print((swordTracker.position - swordPos).magnitude / Time.deltaTime);
        if ((currentPos - previousPos).magnitude / Time.deltaTime > minSpeed && !slashing)
        {
            StartSlash();
        }
        else if((currentPos - previousPos).magnitude / Time.deltaTime <= minSpeed && slashing)
        {
            EndSlash();
        }
        previousPos = currentPos;

        if (slashing)
        {
            slashTimer += Time.deltaTime;
            if(slashTimer >(1/ UpdateInterval))
            {
                currentSlash.UpadateSlash(currentPos, slashOffset);
            }
        }
    }


    private void Initialized()
    {
        currentPos = swordTracker.InverseTransformPoint(transform.position + transform.forward * swordLength);
        previousPos = currentPos;
        fakeEnemy = FindObjectOfType<FakeEnemy>();
        float startX = -fakeEnemy.cameraLength / 2;
        for(int i = 0; i < fakeEnemy.pathsNum; i++)
        {
            SwordArea area = new SwordArea();
            area.number = i;
            area.leftX = startX;
            startX += fakeEnemy.cameraLength / fakeEnemy.pathsNum;
            area.rightX = startX;
            area.lowY = 0;
            area.highY = area.lowY + fakeEnemy.CameraHigh;
            area.swordPos = new Vector3((area.rightX + area.leftX) * 3 / 4, (area.lowY + area.highY) * 1 / 4, -14);
            swordAreas.Add(area);
        }
    }
    public void StartSlash()
    {
        print("StartSlash");
        slashing = true;
        foreach(var item in swordAreas)
        {
            if (item.InArea(previousPos))
            {
                position = item.number;
                this.transform.position = item.swordPos;
            }
        }
        slashOffset = swordTracker.position - this.transform.position;
        slashOffset.z = 0;
        currentSlash = Instantiate(SlashPrefab).GetComponent<Slash>();
    }
    public void EndSlash()
    {
        print("StopSlash");
        slashing = false;
    }
    /// <summary>
    /// use the starting point of the slash to find the right position of sword(in which paths
    /// </summary>
    public void ResetSwordPosition()
    {

    }
}
