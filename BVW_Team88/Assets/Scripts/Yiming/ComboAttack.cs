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
    public Transform projectPlaneZOffset;
    public GameObject ComboAttackEffect;
    Vector3 zOffset;
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
        zOffset = new Vector3(0, 0, projectPlaneZOffset.position.z);
    }

    public void TrackTwoSwordPosition()
    {
        if(players[0].playerState != Player.State.CombineAttack && players[1].playerState != Player.State.CombineAttack)
        {
            slashStartPoint = Vector3.zero;
            slashCurrentPoint = Vector3.zero;
            isNear = false;

        }
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
                UpdateallPlayerswords();
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
                            attackDecisionBar.itsBoss.BossWasAttacked(120);
                            if (ComboAttackEffect)
                            {
                                SoundFXManager.instance.PlayPowerUp();
                                Instantiate(ComboAttackEffect, zOffset + Vector3.forward * 0.3f - Vector3.up * 2.5f, Quaternion.identity);
                            }
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
                            attackDecisionBar.canMove = false;
                            attackDecisionBar.itsBoss.BossAttack();
                        }
                    }
                }
                UpdateallPlayerswords();
            }
        }
    }

    public void UpdateallPlayerswords()
    {
        for (int i = 0; i < players.Length; i++)
        {
            UpdateSwordsSpritePlace(players[i], swordTrackers[i].position);
        }
    }
    public void UpdateSwordsSpritePlace(Player player,Vector3 tracker)
    {
        player.childSword.SwordTransform.position = Vector3.ProjectOnPlane(tracker, new Vector3(0, 0, 1)) + zOffset;

    }

}
