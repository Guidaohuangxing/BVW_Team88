using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //public Text lifeTxt;
    public Text ScoreTxt;
    public ManageScenes manageScenes;
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

    public bool isWin = false;


    private void Start()
    {
        Initialized();
    }
    private void Update()
    {
        //lifeTxt.text = "Total Life :" + life;
        ScoreTxt.text = "Score :" + score;
        //CheckTwoPlayerPosition();
        CheckEnding();
    }
    public void GotDamage(int damage)
    {
        life -= damage;
    }
    
    public void GetScore(int s)
    {
        score += s;
    }


    public bool CheckTwoPlayersHealth()
    {
        if(players[0].Health<=0 && players[1].Health <= 0)
        {
            return true;
        }
        return false;
    }



    ///// <summary>
    ///// simple check will polish that later
    ///// </summary>
    //public void CheckTwoPlayerPosition()
    //{
    //   if( players[0].position == players[1].position)
    //    {
    //        int upperPosition = players[0].position;
    //        players[0].isRide = true;
    //        players[0].SwordStartPosition = upperSwordAreas[upperPosition].swordPos.position;
    //        players[0].transform.position = upperSwordAreas[upperPosition].playerPos.position;
    //        players[0].position = upperSwordAreas[upperPosition].number;
    //    }
    //    else
    //    {
    //        players[0].isRide = false;
    //    }
    //}
    private void Initialized()
    {
        players = playerParent.GetComponentsInChildren<Player>();
    }

    private void CheckEnding()
    {
        //use two players health to decide when to died
        if (CheckTwoPlayersHealth())
        {
            manageScenes.GoToLose();
        }
        else if (isWin)
        {
            manageScenes.GoToWin();
        }
       
      
    }

    //void CheckIfDead()
    //{
    //    p1Dead = ph1.CheckHealth();
    //    p2Dead = ph2.CheckHealth();

    //    if (p1Dead && p2Dead)
    //    {
    //        manageScenes.GoToLose();
    //    }

    //}
}
