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
    public List<GameObject> delayReshowObject;//something like UI or weapon Sprite,will not show at frist
  
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

    public void SetPlayerStateToCertainState(Player.State state)
    {
        players[0].playerState = state;
        players[1].playerState = state;
    }

    public void ResetPlayerCombo()
    {
        players[0].ResetCombo();
        players[1].ResetCombo();
    }


    private void Initialized()
    {
        players = playerParent.GetComponentsInChildren<Player>();
        
        spawn = FindObjectOfType<SpawnAdvance>(); 
        bossController = FindObjectOfType<Boss>();
        foreach(var item in delayReshowObject)
        {
            item.SetActive(false);
        }
    }

    private void CheckEnding()
    {
        if (isWin)
        {
            SetPlayerStateToCertainState(Player.State.Wait);
            StartCoroutine(JumpToWin());
           
        }  
    }
    IEnumerator JumpToWin()
    {
        yield return new WaitForSeconds(5f);
        manageScenes.GoToWin();
    }
    
    public void StartGame()
    {
        spawn.spawnState = SpawnAdvance.SpawnState.normal;
        SetPlayerStateToCertainState(Player.State.Alive);
        players[0].combo = 0;
        players[1].combo = 0;
        foreach (var item in delayReshowObject)
        {
            item.SetActive(true);
        }
    }
}
