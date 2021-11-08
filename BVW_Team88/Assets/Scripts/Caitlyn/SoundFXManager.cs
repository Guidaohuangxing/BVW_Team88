using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public AudioSource slice, hit, monster;

    public AudioClip monsterMad, monsterNormal, sliceNoise, hitByMonster;
    public List<AudioClip> randomHit = new List<AudioClip>();

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

    public void PlayHit()
    {
        int i = Random.Range(0, randomHit.Count);
        hit.PlayOneShot(randomHit[i]);
    }

}
