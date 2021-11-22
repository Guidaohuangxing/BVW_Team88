using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackAdvance : MonoBehaviour
{
   [System.Serializable]
   public enum ComboState
    {
        TwoWeaponMove,
        TwoWeaponCloseEnough,
        CombineAnimation,
        OneWeaponMove,
        wait
    }

    public ComboState comboState = ComboState.wait;

    public Player[] players;
    public List<Transform> swordTrackers = new List<Transform>();
    public float twoSwordDistance = 0.5f;
    public float slashThreshold = 0.9f;
    public GameObject weapon1, weapon2;//not the weapon objects just the quad
    public GameObject weaponCombineAnimation;//put animation here
    public float closeTimeThreshold = 2f;
    private float timer = 0;
    public Transform combinePlace;//the place play the combine animation;
    public float AnimationPlayThreshold = 0.1f;
    public Transform twoSwordsSpritesOffset;//project offset position
    public GameObject ComboAttackEffect;



    private Vector3 twoSwordsUpdateOffset;
    private Vector3 oneSwordUpdateOffset;
    private Vector3 startPoint;
    private Vector3 currentPoint;
    private AttackDecisionBar attackDecisionBar;
    private void Start()
    {
        Initialized();
    }
    public void Initialized()
    {
        players = GetComponentsInChildren<Player>();
        foreach (var item in players)
        {
            swordTrackers.Add(item.tracker);
        }
        twoSwordsUpdateOffset = new Vector3(0, twoSwordsSpritesOffset.position.y * -1, twoSwordsSpritesOffset.position.z);
        weaponCombineAnimation.SetActive(false);
    }


    private void Update()
    {
        BehaviorBaseOnState();
    }

    public void BehaviorBaseOnState()
    {
        if(comboState == ComboState.wait)
        {
            //almost done nothing
            //check if enter twoWeaponMove
            CheckIfEnterTwoWeaponMove();
        }
        else if(comboState == ComboState.TwoWeaponMove)
        {
            //update position
            UpdateSwordsPosition();
            //track if close enough,then go to close enough state
            CheckIfCloseEnough();    
        }
        else if(comboState == ComboState.TwoWeaponCloseEnough)
        {
            //countTime
            //time enough go to the combine anim
            IfCloseForEnoughTime();
            //too far go back to two weaponMove
            CheckIfFarEnough();
        }
        else if(comboState == ComboState.CombineAnimation)
        {
            //still track vive position
            MoveWeaponsToTheCombinePlace();
            CheckIfFarEnough();
        }
        else if(comboState == ComboState.OneWeaponMove)
        {
            UpdateOneSwordSprite();
            //test slash
            CheckIfSlash();
            //if slash wether is work go back to wait,reset player's combo state;
            CheckIfFarEnough();
        }
    }




    public void CheckIfEnterTwoWeaponMove()
    {
        if (players[0].playerState == Player.State.CombineAttack && players[1].playerState == Player.State.CombineAttack)
        {
            comboState = ComboState.TwoWeaponMove;
            attackDecisionBar = FindObjectOfType<AttackDecisionBar>();
        }
    }



    /// <summary>
    /// update two swords Position
    /// </summary>
    public void UpdateSwordsPosition()
    {
        for (int i = 0; i < players.Length; i++)
        {
            UpdateSwordsSpritePlace(players[i], swordTrackers[i].position);
        }
    }
    public void UpdateSwordsSpritePlace(Player player, Vector3 tracker)
    {
        player.childSword.SwordTransform.position = Vector3.ProjectOnPlane(tracker, new Vector3(0, 0, 1)) + twoSwordsUpdateOffset;

    }
    /// <summary>
    /// enter a collect position
    /// </summary>
    public void CheckIfCloseEnough()
    {
        if(VivesDistance() <= twoSwordDistance)
        {
            comboState = ComboState.TwoWeaponCloseEnough;
        }
    }

    public void CheckIfFarEnough()
    {
        if (VivesDistance() > twoSwordDistance)
        {
            comboState = ComboState.TwoWeaponMove;
            ResetAllSprite();
        }

    }

    public float VivesDistance()
    {
        float d = Vector3.Distance(swordTrackers[0].position, swordTrackers[1].position);
        return d;
    }
    /// <summary>
    /// when go back to two weapon move need to disactive the weaponcombine and 
    /// </summary>
    public void ResetAllSprite()
    {
        weaponCombineAnimation.SetActive(false);
        weapon1.SetActive(true);
        weapon2.SetActive(true);
    }

    public void IfCloseForEnoughTime()
    {
        timer += Time.deltaTime;
        if(timer >= closeTimeThreshold)
        {
            comboState = ComboState.CombineAnimation;
            timer = 0;
        }    
    }

    /// <summary>
    /// move the weapon quad
    /// </summary>
    public void MoveWeaponsToTheCombinePlace()
    {
        if(Vector3.Distance(players[0].childSword.SwordTransform.position, combinePlace.position) > AnimationPlayThreshold && Vector3.Distance(players[1].childSword.SwordTransform.position, combinePlace.position) > AnimationPlayThreshold)
        {
            foreach(var item in players)
            {
                item.childSword.SwordTransform.position = Vector3.Lerp(item.childSword.SwordTransform.position, combinePlace.position, 0.03f);
            }
        }
        else
        {
            StartCombineAnimation();
        }
    }
    /// <summary>
    /// the sprite animation
    /// </summary>
    public void StartCombineAnimation()
    {
        comboState = ComboState.OneWeaponMove;
        weapon1.SetActive(false);
        weapon2.SetActive(false);
        weaponCombineAnimation.SetActive(true);
        weaponCombineAnimation.transform.position = combinePlace.position;
        startPoint = (swordTrackers[0].position + swordTrackers[1].position) / 2;
        oneSwordUpdateOffset = Vector3.ProjectOnPlane(startPoint, new Vector3(0, 0, 1)) - combinePlace.position;
    }

    public void UpdateOneSwordSprite()
    {
        currentPoint = (swordTrackers[0].position + swordTrackers[1].position) / 2;
        weaponCombineAnimation.transform.position = Vector3.ProjectOnPlane(currentPoint, new Vector3(0, 0, 1)) - oneSwordUpdateOffset;
    }

    private void CheckIfSlash()
    {
        if ((currentPoint - startPoint).y < 0 && Vector3.Distance(startPoint, currentPoint) > slashThreshold)
        {
            //success slash ;use decisionBar to decide whether is a effective attack
            if (attackDecisionBar)
            {
                foreach (var item in players)
                {
                    item.playerState = Player.State.Wait;
                }
                comboState = ComboState.wait;
                attackDecisionBar.BeSlashedAndStop();
                if (attackDecisionBar.isIntheRightArea)
                {
                    attackDecisionBar.itsBoss.BossWasAttacked(40);
                    if (ComboAttackEffect)
                    {
                        SoundFXManager.instance.PlayPowerUp();
                        Instantiate(ComboAttackEffect, combinePlace.position + Vector3.forward * 0.3f - Vector3.up * 2.5f, Quaternion.identity);
                    }
                   
                }
                else
                {
                    attackDecisionBar.canMove = false;
                    attackDecisionBar.itsBoss.BossAttack();
                }
            }
        }
    }
}
