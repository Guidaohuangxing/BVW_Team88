using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioSource slice, hit, monster;

    public AudioClip monsterMad, monsterNormal, sliceNoise, hitByMonster;

    public void PlayMonsterMad() {
        monster.PlayOneShot(monsterMad);
    }
    public void PlayMonsterNormal()
    {
        monster.PlayOneShot(monsterNormal);
    }
    public void PlaySliceSound() {
        slice.PlayOneShot(sliceNoise);
    }
    public void PlayMiss() {
        hit.PlayOneShot(hitByMonster);
    }

}
