using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : MonoBehaviour
{
    public List<GameObject> AttackObjects;
    public int pathsNum = 3; //devide the space in 3 paths
    public float pathsLength = 30;//get the screens width
    public float cameraLength = 3.2f;
    public List<Vector3> targetPos = new List<Vector3>(); 
    public float minGenerateGapTime = 3;
    public float maxGenerateGapTime = 7;
    public bool isGenerate = false;
    private float[] times;
    private float thresholdTime;
    private List<Vector3> GeneratePosition = new List<Vector3>();
    public float high = 0.1f;
    public float CameraHigh = 5f;
    public List<GameObject> Gos = new List<GameObject>();
    private void Start()
    {
        Initialized();
    }

    private void Update()
    {
        for(int i = 0; i < pathsNum; i++)
        {
            times[i] += Time.deltaTime;
            thresholdTime = Random.Range(minGenerateGapTime, maxGenerateGapTime);
            if (times[i] >= thresholdTime)
            {
                int objectNum = (Random.Range(0, AttackObjects.Count));
                GameObject go = Instantiate(AttackObjects[objectNum], GeneratePosition[i], Quaternion.identity);
                go.GetComponent<AttackObject>().targetPosition = targetPos[i] - new Vector3(0, 0, 3);
                Gos.Add(go);
                times[i] = 0;
            }
        }
        foreach (var item in Gos)
        {
            if (item != null) { item.GetComponent<AttackObject>().MoveToPlayer(); }
        }
    }
    private void Initialized()
    {
       Vector3 gPos = this.transform.position - new Vector3(pathsLength / 2 - pathsLength/(2* pathsNum), -high, 0);
       for(int i = 0; i< pathsNum; i++)
        {
            GeneratePosition.Add(gPos);
            gPos += new Vector3(pathsLength / pathsNum, 0, 0);
        }
        Vector3 tPos = Camera.main.transform.position - new Vector3(cameraLength / 2 - cameraLength / (2 * pathsNum), -high, 0);
        for (int i = 0; i < pathsNum; i++)
        {
            targetPos.Add(tPos);
            tPos += new Vector3(cameraLength / pathsNum, 0, 0);
        }
        isGenerate = true;
        times = new float[pathsNum];
    }

}
