using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public Player[] players;
    public List<Transform> swordTrackers = new List<Transform>();
    public AttackDecisionBar attackDecisionBar;//each time can attack then find this
    public float distanceThreshold = 0.1f;//two distance of two vivetracker
    public float slashThreshold = 1f;
    public bool isNear = false;//if two player's tracker is near enough than 
    private Vector3 slashStartPoint;
    private Vector3 slashCurrentPoint;
    private void Start()
    {
        Instantiate();
    }

    private void Update()
    {
        TrackTwoSwordPosition();
    }
    
    private void Instantiate()
    {
        players = GetComponentsInChildren<Player>();
        foreach(var item in players)
        {
            swordTrackers.Add(item.tracker);
        }
    }

    public void TrackTwoSwordPosition()
    {
        if(players[0].playerState ==Player.State.CombineAttack && players[1].playerState == Player.State.CombineAttack)
        {
            attackDecisionBar = FindObjectOfType<AttackDecisionBar>();
            if (!isNear)
            {
                if (Vector3.Distance(swordTrackers[0].position, swordTrackers[1].position) <= distanceThreshold)
                {
                    isNear = true;
                    slashStartPoint = (swordTrackers[0].position + swordTrackers[1].position) / 2;
                    //show combine animation and enter attack mode       
                }
                //tracker distance
                for (int i = 0; i < players.Length; i++)
                {
                    UpdateSwordsSpritePlace(players[i], swordTrackers[i].position);
                }  
            }
            else if (isNear)
            {
                if(slashStartPoint == null) { slashStartPoint = (swordTrackers[0].position + swordTrackers[1].position) / 2; }
                if(Vector3.Distance(swordTrackers[0].position, swordTrackers[1].position) > distanceThreshold)
                {
                    isNear = false;
                }
                slashCurrentPoint = (swordTrackers[0].position + swordTrackers[1].position) / 2;
                if ((slashCurrentPoint - slashStartPoint).y < 0 && Vector3.Distance(slashStartPoint, slashCurrentPoint) > slashThreshold)
                {
                    //success slash ;use decisionBar to decide whether is a effective attack
                    if (attackDecisionBar)
                    {
                        attackDecisionBar.BeSlashedAndStop();
                        if (attackDecisionBar.isIntheRightArea)
                        {
                            attackDecisionBar.itsBoss.BossWasAttacked(40);
                            foreach(var item in players)
                            {
                                item.playerState = Player.State.Wait;
                            }
                        }
                        else
                        {
                            foreach (var item in players)
                            {
                                item.playerState = Player.State.Wait;
                            }
                            attackDecisionBar.DestoryAllBar();
                        }
                    }
                }

            }
        }
    }


    public void UpdateSwordsSpritePlace(Player player,Vector3 tracker)
    {

    }

}
