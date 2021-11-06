using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class SwordArea
    {
        public int number;
        public float leftX;
        public float rightX;
        public float highY;
        public float lowY;
        public Vector3 swordPos;
        public Vector3 playerPos;
        public bool InArea(Vector3 point)
        {
            if (point.x <= rightX && point.x >= leftX && point.y <= highY && point.y >= lowY)
            {
                return true;
            }
            return false;
        }
    }

    public List<SwordArea> swordAreas = new List<SwordArea>();

    public float playerPosLength = 5;
    public float cameraLength = 2;
    public float pathsNum = 3;
    public float cameraHigh = 3;

    // Start is called before the first frame update
    void Start()
    {
        float startX = - cameraLength / 2;
        float posX = -playerPosLength / 2;
        for (int i = 0; i < pathsNum; i++)
        {
            SwordArea area = new SwordArea();
            area.number = i;
            area.leftX = startX;
            startX += cameraLength / pathsNum;
            area.rightX = startX;
            area.lowY = 0;
            area.highY = area.lowY + cameraHigh;
            float lx = posX;
            posX += playerPosLength / pathsNum;
            float rx = posX;
            area.swordPos = new Vector3((rx + lx) * 2 / 4, (area.lowY + area.highY) * .5f / 4, -14);
            area.playerPos = new Vector3((rx + lx) * 2 / 4, (area.lowY + area.highY) * 0, -15);
            swordAreas.Add(area);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
