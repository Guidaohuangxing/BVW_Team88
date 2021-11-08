using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text lifeTxt;
    public Text ScoreTxt;

    [System.Serializable]
    public class SwordArea
    {
        public int number;
        public float leftX;
        public float rightX;
        public float highY;
        public float lowY;
        public Transform swordPos;
        public Transform playerPos;
        public bool InArea(Vector3 point)
        {
            if (point.x < rightX && point.x > leftX)
            {
                return true;
            }
            return false;
        }
    }

    public List<SwordArea> swordAreas = new List<SwordArea>();
    public List<SwordArea> upperSwordAreas = new List<SwordArea>();

    public int life = 100;
    public int score = 0;

    private Player[] players = new Player[2];
    public GameObject playerParent;

    
    public float pathsNum = 3;
    private void Start()
    {
        Initialized();
    }
    private void Update()
    {
        lifeTxt.text = "Total Life :" + life;
        ScoreTxt.text = "Score :" + score;
        CheckTwoPlayerPosition();
    }
    public void GotDamage(int damage)
    {
        life -= damage;
    }
    
    public void GetScore(int s)
    {
        score += s;
    }
    public void CheckTwoPlayerPosition()
    {
       if( players[0].position == players[1].position)
        {
            int upperPosition = players[0].position;
            players[0].isRide = true;
            players[0].SwordStartPosition = upperSwordAreas[upperPosition].swordPos.position;
            players[0].transform.position = upperSwordAreas[upperPosition].playerPos.position;
        }
        else
        {
            players[0].isRide = false;
        }
    }
    private void Initialized()
    {
        players = playerParent.GetComponentsInChildren<Player>();
    }
}
