using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMechanics : MonoBehaviour
{
    public int monsterHp = 100;
    public Slider monsterHealthbar;
    public ManageScenes ms;
    public SoundFXManager sfx;
    public bool PowerUpReady= false;
    public bool PowerUpUsed = false;

    private bool bossIsAttackable, round2, round3, bossDead = false;
    private bool round1 = true;

    // Start is called before the first frame update
    void Start()
    {
        monsterHealthbar.value = monsterHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerUpReady) {
            //move forward slowly
        }
        if (PowerUpUsed) { 
            // move to position that players can attack you at
        }
    }

    public void DamageBoss(int damage) {
        if (bossIsAttackable) {
            sfx.PlayMonsterHurt();
            monsterHp -= damage; 
            monsterHealthbar.value = monsterHp;
        }
        if (monsterHp <= 0) {
            bossDead = true;
            BossDie();
        }
    }
   
    public void BossApproach() {
        PowerUpReady = true; 
    }

    public void StunBoss() {

        bossIsAttackable = true;

        //Boss Stun Animation

        DamageBoss(15);
        if (!bossDead)
        {
            if (round1)
            {
                StartCoroutine(BossRecover(5f));
            }
            else if (round2)
            {
                StartCoroutine(BossRecover(4f));
            }
            else if (round3)
            {
                StartCoroutine(BossRecover(3f));
            }
        }

    }

    public void StartPK() { 
        //Shake world maybe?
        //Extreme angery bellow
        //instantiate huge object
    }
    
    IEnumerator BossRecover(float recoveryTime)
    {
        yield return new WaitForSeconds(recoveryTime);
        bossIsAttackable = false;
        //MonsterShriek with anger
        sfx.PlayMonsterMad();
        if (round1)
        {
            round1 = false;
            round2 = true;
        }
        else if (round2)
        {
            round2 = false;
            round3 = true;
        }
        else if (round3) {
            StartPK();
        }


    }

    IEnumerator BossDie()
    {
        //Boss dying sfx
        sfx.PlayMonsterDeath();
        //Boss dying animation
        //this.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        ms.GoToWin();

    }


}
