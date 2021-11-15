using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public ManageScenes ms;
    public AudioClip startClip;
    public AudioSource BGM;
    public SoundFXManager sfx;
    public GameObject instrCanvas;
    public GameObject introCanvas;

    public void StartGameUp() {
        StartCoroutine(PlayAnnouncer());
    }

    IEnumerator PlayAnnouncer() {
        BGM.volume = 0.30f;
        sfx.PlayStartNoise();
        yield return new WaitForSeconds(startClip.length);
        BGM.volume = 0.30f;
        //ms.GoToInstructions();
        instrCanvas.SetActive(true);
        introCanvas.SetActive(false);

    }
}
