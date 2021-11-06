using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpawnBasedOnBPM : MonoBehaviour
{
    public float bpm = 96.0f;
    public float bpmRatio = 1.0f;
    public float bpmSpawn;
    public GameObject spawnPointP1, spawnPointP2, spawnPointTentaclesUp, spawnPointTentaclesLow;
    public GameObject trash1Prefab, trash2Prefab, trash3Prefab, trash4Prefab, tentaclePrefab;
    public GameObject player1, player2, stackUp;
    //public GameObject p1Weapon, p2Weapon, p3Weapon, p4Weapon, stackLower, stackHigher;
    public float raiseDifficultyTime = 20.0f;

    private bool p1T1, p1T2, p1T3, p1T4, p2T1, p2T2, p2T3, p2T4=false;
    private bool round1, round2, round3=false;
    private float time = 0;
    private float beatTimer = 0;

    public List<GameObject> p1Obj = new List<GameObject>();
    public List<GameObject> p2Obj = new List<GameObject>();
    public List<GameObject> p3Obj = new List<GameObject>();
    public List<GameObject> p4Obj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        round1 = true;
        time = Time.time;
        bpmSpawn = (60 / bpm) * bpmRatio;
        SetPlayerType();
    }

    // Update is called once per frame
    void Update()
    {
        if (bpmSpawn < beatTimer)
        {
            int randomNumber = 1;
            //finding random lane to spawn in

            randomNumber = generateRandom(2);
            GameObject instance;

            //spawning the object
            if (randomNumber == 1)
            {
                if (p1T1) {
                    instance = Instantiate(trash1Prefab, spawnPointP1.transform.position, spawnPointP1.transform.rotation);
                    p1Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(1);
                }
                else if (p1T2)
                {
                    instance = Instantiate(trash2Prefab, spawnPointP1.transform.position, spawnPointP1.transform.rotation);
                    p2Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(1);
                }
                else if (p1T3)
                {
                    instance = Instantiate(trash3Prefab, spawnPointP1.transform.position, spawnPointP1.transform.rotation);
                    p3Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(1);
                }
                else if (p1T4)
                {
                    instance = Instantiate(trash4Prefab, spawnPointP1.transform.position, spawnPointP1.transform.rotation);
                    p4Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(1);
                }


            }
            else if (randomNumber == 2)
            {
                if (p2T1)
                {
                    instance = Instantiate(trash1Prefab, spawnPointP2.transform.position, spawnPointP2.transform.rotation);
                    p1Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(2);
                }
                else if (p2T2)
                {
                    instance = Instantiate(trash2Prefab, spawnPointP2.transform.position, spawnPointP2.transform.rotation);
                    p2Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(2);
                }
                else if (p2T3)
                {
                    instance = Instantiate(trash3Prefab, spawnPointP2.transform.position, spawnPointP2.transform.rotation);
                    p3Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(2);
                }
                else if (p2T4)
                {
                    instance = Instantiate(trash4Prefab, spawnPointP2.transform.position, spawnPointP2.transform.rotation);
                    p4Obj.Add(instance);
                    instance.GetComponent<BeatMovement>().SetLane(2);
                }
            }

            beatTimer -= bpmSpawn;
        }
        beatTimer += Time.deltaTime;

        //if (Time.time >= time + raiseDifficultyTime)
        //{
        //    BossAttack();
        //    changeDifficulty();
        //    time = Time.time;
        //}
    }

    void changeDifficulty()
    {
        if (round1)
        {
            round1 = false;
            round2 = true;
            bpmRatio = 1f;
            bpmSpawn = (60 / bpm) * bpmRatio;
        }
        else if (round2)
        {
            round2 = false;
            round3 = true; 
            //change bpm value
            bpmRatio = 0.5f;
            bpmSpawn = (60 / bpm) * bpmRatio;
            //potentially speed up movement of spawns
        }
    }

    int generateRandom(int numOfDrums)
    {
        int num = 0;
        float randNum = 0f;

        if (numOfDrums == 2) { randNum = Random.Range(1.0f, 2.0f); }
        if (randNum < 1.5)
        {
            num = 1;
        }
        else if (randNum >= 1.5 && randNum < 2.5)
        {
            num = 2;
        }
        return num;
    }

    // Deletes note and returns number of points it was worth
    // Returns -1 on no deletion
    public int RemoveFromList(string objType) {
        GameObject instance;

        int points = -1;

        if (objType == "1") {
            if (p1Obj.Count != 0)
            {
                instance = p1Obj[0];
                if (instance.GetComponent<BeatMovement>().GetCollectable()) {
                    points = instance.GetComponent<BeatMovement>().GetPoints();
                    p1Obj.RemoveAt(0);
                    Destroy(instance);
                    return points;
                }
                
            }
        }
        else if (objType == "2")
        {
            if (p2Obj.Count != 0)
            {
                instance = p2Obj[0];
                if (instance.GetComponent<BeatMovement>().GetCollectable())
                {
                    points = instance.GetComponent<BeatMovement>().GetPoints();
                    p2Obj.RemoveAt(0);
                    Destroy(instance);
                    return points;
                }

            }
        }
        else if (objType == "3")
        {
            if (p3Obj.Count != 0)
            {
                instance = p3Obj[0];
                if (instance.GetComponent<BeatMovement>().GetCollectable())
                {
                    points = instance.GetComponent<BeatMovement>().GetPoints();
                    p3Obj.RemoveAt(0);
                    Destroy(instance);
                    return points;
                }

            }
        }
        else if (objType == "4")
        {
            if (p4Obj.Count != 0)
            {
                instance = p4Obj[0];
                if (instance.GetComponent<BeatMovement>().GetCollectable())
                {
                    points = instance.GetComponent<BeatMovement>().GetPoints();
                    p4Obj.RemoveAt(0);
                    Destroy(instance);
                    return points;
                }

            }
        }
        return points;
    }

    void SetPlayerType() {
        if (player1.gameObject.tag == "Type1") {
            p1T1 = true;
        }else if (player1.gameObject.tag == "Type2")
        {
            p1T2 = true;
        }
        else if (player1.gameObject.tag == "Type3")
        {
            p1T3 = true;
        }
        else if (player1.gameObject.tag == "Type4")
        {
            p1T4 = true;
        }

        if (player2.gameObject.tag == "Type1")
        {
            p2T1 = true;
        }
        else if (player2.gameObject.tag == "Type2")
        {
            p2T2 = true;
        }
        else if (player2.gameObject.tag == "Type3")
        {
            p2T3 = true;
        }
        else if (player2.gameObject.tag == "Type4")
        {
            p2T4 = true;
        }
    }

    void BossAttack() {
        Debug.Log("Add Boss Attack in center lane"); 
    }
}
