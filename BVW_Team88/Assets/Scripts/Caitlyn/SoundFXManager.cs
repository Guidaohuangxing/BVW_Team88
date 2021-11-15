using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    public AudioSource slice, hit, monster, announcer;

    public AudioClip monsterMad, monsterNormal, monsterDeath, monsterHurt, monsterEnraged,sliceNoise, hitByMonster;
    public AudioClip defeatClip, startClip, timeIsRunningOut, victoryClip, comboClip;
    public AudioClip powerupAttack, raccoonEating;
    public List<AudioClip> randomHit = new List<AudioClip>();
    public List<AudioClip> randomPraise = new List<AudioClip>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Monster sounds ----------------------------------------------
    public void PlayMonsterMad() {
        monster.PlayOneShot(monsterMad);
    }
    public void PlayMonsterNormal()
    {
        monster.PlayOneShot(monsterNormal);
    }
    public void PlayMonsterDeath()
    {
        monster.PlayOneShot(monsterDeath, 3f);
    }
    public void PlayMonsterHurt()
    {
        monster.PlayOneShot(monsterHurt, 3f);
    }
    public void PlayMonsterEnraged()
    {
        monster.PlayOneShot(monsterEnraged);
    }

    //-----------------------------------------------------------



    //Sword/player sounds----------------------------------------
    public void PlaySliceSound() {
        slice.PlayOneShot(sliceNoise);
    }
    public void PlayMiss() {
        hit.PlayOneShot(hitByMonster);
    }

    public void PlayHit()
    {
        int i = Random.Range(0, randomHit.Count);
        hit.PlayOneShot(randomHit[i]);
    }
    //------------------------------------------------------------



    //Instruction/Commentator sounds------------------------------
    public void PlayComboPraise()
    {
        
        int i = Random.Range(0, randomPraise.Count);
        announcer.PlayOneShot(randomPraise[i]);
    }
    public void PlayLoseNoise()
    {
        announcer.PlayOneShot(defeatClip);
    }
    public void PlayWinNoise()
    {
        announcer.PlayOneShot(victoryClip);
    }
    public void PlayStartNoise()
    {
        announcer.PlayOneShot(startClip);
    }
    public void PlayTimeWarning()
    {
        announcer.PlayOneShot(timeIsRunningOut);
    }
    public void PlayCombo() {
        announcer.PlayOneShot(comboClip);
    }

    

    //-------------------------------------------------------------



    //Other sounds ------------------------------------------------
    public void PlayPowerUp()
    {
        announcer.PlayOneShot(powerupAttack);
    }

    public void RacconEating()
    {
        monster.PlayOneShot(raccoonEating);
    }
    //-------------------------------------------------------------
}
