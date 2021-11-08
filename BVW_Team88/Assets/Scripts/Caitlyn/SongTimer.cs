using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongTimer : MonoBehaviour
{
    public AudioClip bgm;
    public ManageScenes ms;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountDown() {
        yield return new WaitForSeconds(bgm.length);
        ms.GoToWin();
    }
}
