using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMechanics : MonoBehaviour
{
    public int monsterHp = 100;
    public AudioSource bgm;
    public Slider monsterHealthbar;
    public Text monsterHpText;
    public ManageScenes ms;
    public SoundFXManager sfx;
    public bool PowerUpReady,PowerUpUsed, bossIsAttackable = false;

    private bool round2, round3, bossDead = false;
    private bool round1 = true;
    private Vector3 approachDest = new Vector3(-0.136009991f, -2.01999998f, -7.93225527f);

    // Start is called before the first frame update
    void Start()
    {
        monsterHealthbar.value = monsterHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (PowerUpReady) {
            this.transform.position = Vector3.Lerp(this.transform.position, approachDest, 0.1f* Time.deltaTime);
        }
        if (PowerUpUsed) { 
            // move to position that players can attack you at
        }
    }

    public void DamageBoss(int damage) {
        if (bossIsAttackable) {
            sfx.PlayMonsterHurt();
            monsterHp = monsterHp - damage; 
            monsterHealthbar.value = monsterHp;
            monsterHpText.text = monsterHp + "/100";
        }
        if (monsterHp <= 0) {
            Debug.Log("Monster hp = 0");
            monsterHpText.text = "0/100";
            bossDead = true;
            StartCoroutine(BossDie());
        }
    }
   
    public void BossApproach() {
        PowerUpReady = true; 
    }
    public void PowerUsed() {
        PowerUpUsed = true;
        PowerUpReady = false;
        StunBoss();
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
        sfx.PlayMonsterEnraged();
        //instantiate huge object
    }
    
    IEnumerator BossRecover(float recoveryTime)
    {
        yield return new WaitForSeconds(recoveryTime);
        bossIsAttackable = false;
        PowerUpUsed = false;
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

        bgm.volume = .3f;
        //Boss dying sfx
        sfx.PlayMonsterDeath();
        //Boss dying animation
        yield return new WaitForSeconds(8f);
        //this.gameObject.SetActive(false);
        ms.GoToWin();

    }


}
