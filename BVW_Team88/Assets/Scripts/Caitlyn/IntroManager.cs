using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public ManageScenes ms;
    public GameObject anim1, anim2; 
    public void StartGameUp() {
        StartCoroutine(SwitchAnimations());
    }

    IEnumerator SwitchAnimations() {
        anim2.SetActive(true);
        anim1.SetActive(false);

        yield return new WaitForSeconds(2.25f);

        ms.GoToInstructions();

    }
}
