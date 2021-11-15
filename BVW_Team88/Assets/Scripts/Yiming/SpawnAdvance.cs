using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAdvance : MonoBehaviour
{
    public float bpm = 96.0f;
    public float bpmSpawn;

    [System.Serializable]
    public enum SpawnState
    {
        normal,//random spawn define by round and lanes
        single,//only spawn in certain lane
        stop//dont spawn until restart
    }

    public SpawnState spawnState = SpawnState.stop;

    [System.Serializable]
    public struct Round
    {
        public int roundNum;//which round right now
        public List<Lane> spawnLanes;//have 2 or more lanes
        public List<GameObject> spawnObjects;//decide the objects this round spawn and the ratio of spawn
        public List<GameObject> healingObjects;//
        public float bpmRatio;// the speed to spawn the objects
        public float bpm;//if need adjust than adjust this one
        public AudioClip bgm;//put some bgm in it;
    }

    public int currentRoundsNumber = 0;//A pointer to check which are this;
    public List<Round> rounds = new List<Round>();
    public List<GameObject> objectsWasSpawn = new List<GameObject>();
    private float timer = 0;//calculate time

    private void Update()
    {
        SpawnObjectsByState(spawnState);
        MoveSpawnObjects(objectsWasSpawn);
    }


    /// <summary>
    /// base on state to spawnObjects
    /// </summary>
    /// <param name="State"></param>
    public void SpawnObjectsByState(SpawnState State)
    {
        if(State != SpawnState.stop && currentRoundsNumber < rounds.Count)
        {
            bpmSpawn = SetBpmSpawn(rounds[currentRoundsNumber].bpm, rounds[currentRoundsNumber].bpmRatio);
            timer += Time.deltaTime;
            if(timer>= bpmSpawn)
            {
                if(State == SpawnState.normal)
                {
                    SpawnObjects(rounds[currentRoundsNumber].spawnLanes, rounds[currentRoundsNumber].spawnObjects);
                }
                else if(State == SpawnState.single)
                {
                    List<Lane> lanes = new List<Lane>();
                    foreach(var item in rounds[currentRoundsNumber].spawnLanes)
                    {
                        if(item.belongPlayer.playerState != Player.State.Dying)
                        {
                            lanes.Add(item);
                        }
                    }
                    SpawnObjects(lanes, rounds[currentRoundsNumber].healingObjects);
                }
                timer = 0;
            }
        }
    }

    /// <summary>
    /// spawn object base on lanes and spawnlist
    /// </summary>
    /// <param name="lanes"></param>
    /// <param name="spawnObjects"></param>
    private void SpawnObjects(List<Lane> lanes, List<GameObject> spawnObjects)
    {
        int lanesPointer = Random.Range(0, lanes.Count);
        int spawnObjectPointer = Random.Range(0, spawnObjects.Count);
        GameObject go = Instantiate(spawnObjects[spawnObjectPointer], lanes[lanesPointer].startPos.position, Quaternion.identity);
        go.GetComponent<AttackObject>().targetPosition = lanes[lanesPointer].endPos.position;
        go.GetComponent<AttackObject>().lane = lanes[lanesPointer].LaneNum;//give the upper lane different lane num
        go.GetComponent<AttackObject>().setLane = true;
        Material[] ms = go.GetComponent<Renderer>().materials;
        foreach (var item in ms)
        {
            item.SetFloat("_EdgePower", 100f);
        }
        if (lanes[lanesPointer].objectSpeed != 0)
        {
            go.GetComponent<AttackObject>().speed = lanes[lanesPointer].objectSpeed;
        }
        objectsWasSpawn.Add(go);
    }

    /// <summary>
    /// move all spawn objects
    /// </summary>
    /// <param name="allSpawnObjects"></param>
    public void MoveSpawnObjects(List<GameObject> allSpawnObjects)
    {
        foreach (var item in allSpawnObjects)
        {
            if (item != null) { item.GetComponent<AttackObject>().MoveToPlayer(); }
        }
    }


    private float SetBpmSpawn(float bpm, float bpmRatio)
    {
        float bpmspawn = (60 / bpm) * bpmRatio;
        return bpmspawn;
    }
}
