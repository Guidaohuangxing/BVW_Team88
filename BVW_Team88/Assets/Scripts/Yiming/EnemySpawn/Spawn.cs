using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public SoundFXManager soundFXManager;
    
    
    public float bpm = 96.0f;
    public float bpmRatio = 1.0f;
    public float bpmSpawn;

    [System.Serializable]
    public class Lane
    {
        public int LaneNum;
        public Transform startPos;
        public Transform endPos;
        public float objectSpeed;
    }
    [System.Serializable]
    public class Round
    {
        public float bpmRatio;
    }

    public List<Lane> lanes;
    public List<Lane> upperLanes;
    public List<GameObject> spawnObjects;
    public List<GameObject> spawnCooperateObjects;//need two people cut at the same time
    public List<GameObject> allSpawnObjects;
    public List<Round> rounds;
    public int roundPointer = 0;
    public List<float> upperLaneTime;


    private float musicTimer = 0;//to calculate if we need to end the game
    private float timer = 0;//time counter
    private float DifficultChangeTimer = 0;
    private bool isUpperLane = false;
    private bool isCooperateObjects = false;
    private int upperLaneTimerPointer = 0;
    private void Start()
    {
        Initialized();
    }

    private void Update()
    {
        //smusicTimer += Time.deltaTime;
        timer += Time.deltaTime;

        DifficultChangeTimer += Time.deltaTime;
        if (timer >= bpmSpawn)
        {
            if (!isUpperLane)
            {
                if (!isCooperateObjects)
                {
                    SpawnObjects(lanes);
                }
            }
            else
            {
                SpawnObjects(upperLanes);
                //isUpperLane = false;
            }
            timer = 0;
        }

        MoveAllObjects();
        //upperLaneTimerPointer = ChangeDifficult(upperLaneTimerPointer);
    }

    /// <summary>
    /// reset the 
    /// </summary>
    public void Initialized()
    {
        bpmRatio = rounds[roundPointer].bpmRatio;
        bpmSpawn = SetBpmSpawn(bpm, bpmRatio);
        allSpawnObjects.Clear();
      
    }
    private float SetBpmSpawn(float bpm, float bpmRatio)
    {
        float bpmspawn = (60 / bpm) * bpmRatio;
        return bpmspawn;
    }

    

    /// <summary>
    /// use to spawn objects
    /// </summary>
    public void SpawnObjects(List<Lane> lanes)
    {
        int lanesPointer = Random.Range(0, lanes.Count);
        int spawnObjectPointer = Random.Range(0, spawnObjects.Count);
        GameObject go = Instantiate(spawnObjects[spawnObjectPointer], lanes[lanesPointer].startPos.position, Quaternion.identity);
        go.GetComponent<AttackObject>().targetPosition = lanes[lanesPointer].endPos.position;
        go.GetComponent<AttackObject>().lane = lanes[lanesPointer].LaneNum;//give the upper lane different lane num
        go.GetComponent<AttackObject>().setLane = true;
        if(lanes[lanesPointer].objectSpeed != 0)
        {
            go.GetComponent<AttackObject>().speed = lanes[lanesPointer].objectSpeed;
        }
        allSpawnObjects.Add(go);
    }

    public int ChangeDifficult(int upperLaneTimerPointer)
    {
        if(upperLaneTimerPointer >= upperLaneTime.Count)
        {
            upperLaneTimerPointer = upperLaneTime.Count - 1;
        }
        if(DifficultChangeTimer > upperLaneTime[upperLaneTimerPointer])
        {
            isUpperLane = !isUpperLane;
            if (isUpperLane)
            {
                bpmSpawn = SetBpmSpawn(bpm, rounds[1].bpmRatio);
                soundFXManager.PlayMonsterMad();
            }
            else if (!isUpperLane)
            {
                bpmSpawn = SetBpmSpawn(bpm, rounds[0].bpmRatio);
                soundFXManager.PlayMonsterNormal();
            }
            DifficultChangeTimer = 0;
            upperLaneTimerPointer++;
        }
        return upperLaneTimerPointer;
    }


    /// <summary>
    /// each update move all objects
    /// </summary>
    public void MoveAllObjects()
    {
        foreach(var item in allSpawnObjects)
        {
            if (item != null) { item.GetComponent<AttackObject>().MoveToPlayer(); }
        }
    }
}
