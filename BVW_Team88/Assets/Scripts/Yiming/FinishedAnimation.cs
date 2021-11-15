using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedAnimation : MonoBehaviour
{
    public GameObject thisAnimation;

    public void AnimateEndDestroy()
    {
        Destroy(thisAnimation);
    }

}
