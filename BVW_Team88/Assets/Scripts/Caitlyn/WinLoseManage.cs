using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManage : MonoBehaviour
{
    public AudioSource bgm;
    public SoundFXManager sfx;
    public int winLoseOpt = 0;
    public AudioClip win, lose; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RaiseVolume());  
    }
    IEnumerator RaiseVolume() {
        if (winLoseOpt == 0) {
            sfx.PlayWinNoise();
            yield return new WaitForSeconds(win.length);
            bgm.volume = .5f;

        }
        else if (winLoseOpt == 1) {
            sfx.PlayLoseNoise();
            yield return new WaitForSeconds(lose.length);
            bgm.volume = 0.5f;
        }
    }
}
