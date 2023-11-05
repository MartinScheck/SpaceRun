using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript_MobileApp : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;        //global var can be use to make sth
    public GameObject pauseIcon;
    public GameObject pausePlay;

    // Start is called before the first frame update
    void Start()
    {
        pausePlay.SetActive(false);
        pauseMenu.SetActive(false);
        isPaused = false;
        pauseIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePlay.SetActive(true);
        pauseMenu.SetActive(true);
        pauseIcon.SetActive(false);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePlay.SetActive(false);
        pauseMenu.SetActive(false);
        pauseIcon.SetActive(true);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene_MobileApp");
    }

    public void BacktoMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene_MobileApp");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
