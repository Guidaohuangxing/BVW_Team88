using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToStart() {
        SceneManager.LoadScene(0);
    }
    public void GoToMain() {
        SceneManager.LoadScene(1);
    }
    public void GoToWin() {
        SceneManager.LoadScene(2);
    }
    public void GoToLose() {
        SceneManager.LoadScene(3);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
