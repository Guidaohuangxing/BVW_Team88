using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public PlayerHealth ph1, ph2;

    public bool p1Dead, p2Dead = false;
    
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
    public void GoToInstructions() {
        SceneManager.LoadScene(4);
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
    public void GoToCredits() {
        SceneManager.LoadScene(5);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
