using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedAnimation : MonoBehaviour
{
    public GameObject thisAnimation;
    private Boss bossController;
    private void Start()
    {
        bossController = FindObjectOfType<Boss>();
    }
    public void AnimateEndDestroy()
    {
        Destroy(thisAnimation);
    }

    public void StartBossWasAttack()
    {
        bossController.StartWasAttackedAnimation();
    }

}
