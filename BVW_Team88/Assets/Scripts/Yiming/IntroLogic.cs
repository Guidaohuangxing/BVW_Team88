using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    public Animator bossAnimator;
    private void Start()
    {
        SoundFXManager.instance.RacconEating();
    }
    public void finish2dAnimate()
    {
        bossAnimator.SetTrigger("GameStart");
        Camera.main.transform.rotation = Quaternion.Euler(3.4f, 0, 0);
        this.gameObject.SetActive(false);
    }
}
