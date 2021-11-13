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
        public Transform swordSpritePos;
        //public bool InArea(Vector3 point)
        //{
        //    if (point.x < rightX && point.x > leftX)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }

    public List<SwordArea> swordAreas = new List<SwordArea>();
    public int score = 0;
    public GameObject playerParent;
    private Player[] players = new Player[2];
    public Boss bossController;
    
    
    public bool isWin = false;
    private SpawnAdvance spawn;

    private void Start()
    {
        Initialized();
    }
    private void Update()
    {
        ScoreTxt.text = "Score :" + score;  
        CheckEnding();
        CheckPlayerStates();
    }
 
    
    public void GetScore(int s)
    {
        score += s;
    }


    public void CheckPlayerStates()
    {
        if(players[0].playerState == Player.State.Dying && players[1].playerState == Player.State.Dying)
        {
            manageScenes.GoToLose();
        }
        else if(players[0].playerState == Player.State.Dying || players[1].playerState == Player.State.Dying)
        {
            spawn.spawnState = SpawnAdvance.SpawnState.single;
        }
        else if(players[0].playerState == Player.State.PowerUp && players[1].playerState == Player.State.PowerUp)
        {
            spawn.spawnState = SpawnAdvance.SpawnState.stop;
            //start the boss behaviour
            bossController.StartPreAttack();    
            //FindObjectOfType<BossMechanics>().BossApproach();
        }
        else if(players[0].playerState == Player.State.Alive && players[1].playerState == Player.State.Alive)
        {
            spawn.spawnState = SpawnAdvance.SpawnState.normal;
        }
    }


    /// <summary>
    /// be call by animation clips on certain frame
    /// </summary>
    public void InformPlayerTheyCanComboAttack()
    {
        players[0].playerState = Player.State.CombineAttack;
        players[1].playerState = Player.State.CombineAttack;
        //inform boss at the same time to do attack prepare;  
    }




    private void Initialized()
    {
        players = playerParent.GetComponentsInChildren<Player>();
        spawn = FindObjectOfType<SpawnAdvance>();
        spawn.spawnState = SpawnAdvance.SpawnState.normal;
        bossController = FindObjectOfType<Boss>();
    }

    private void CheckEnding()
    {
        if (isWin)
        {
            manageScenes.GoToWin();
        }  
    }
}
