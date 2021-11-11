using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioSource slice, hit, monster;

    public AudioClip monsterMad, monsterNormal, monsterDeath, monsterHurt, monsterEnraged,sliceNoise, hitByMonster;
    public List<AudioClip> randomHit = new List<AudioClip>();


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

    /* public void PlayComboSound() { 
         combo.PlayOneShot(comboClip);
     }
     public void PlaySuperCombo() { 
         combo.PlayOneShot(superComboClip);
     }
     */
    //-------------------------------------------------------------



    //Other sounds ------------------------------------------------


    //-------------------------------------------------------------
}
