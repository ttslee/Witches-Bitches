using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    //GameObject[] pauseObjects;
    public GameObject mainCam;
    public GameObject pCam;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        //pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        //print(pauseObjects[0].name);
        hidePaused();
    }

    // Update is called once per frame
    void Update()
    {

        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseControl();
        }
    }


    //Reloads the Level
    public void Reload()
    {
        //Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        //foreach (GameObject g in pauseObjects)
        //{
        //    g.SetActive(true);
        //}
        pCam.SetActive(true);
        mainCam.SetActive(false);
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        //foreach (GameObject g in pauseObjects)
        //{
        //    g.SetActive(false);
        //}
        pCam.SetActive(false);
        mainCam.SetActive(true);
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        //Application.LoadLevel(level);
    }
    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }
}
