using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform tracker;
    GameManager gameManager;
    public int position;
    public Vector3 SwordStartPosition;
    public Sword childSword;
    private float timer =0;
    public float threshold = .1f;
    public string tagName;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>= threshold)
        {
            UpdatePosition();
            timer = 0;
        }
        print("excutive this");
    }

    public void UpdatePosition()
    {
        if (!childSword.slashing)
        {
            //print("track area" + tracker.position);
            foreach(var item in gameManager.swordAreas)
            {
                if (item.InArea(tracker.position))
                {
                    position = item.number;
                    SwordStartPosition = item.swordPos;
                    transform.position = item.playerPos;
                }
            }
        }
    }

}
