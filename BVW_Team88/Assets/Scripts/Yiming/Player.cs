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


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }
    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        if (!childSword.slashing)
        {
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
