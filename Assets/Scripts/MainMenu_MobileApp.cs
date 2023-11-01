using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_MobileApp : MonoBehaviour
{

    public GameObject buttonStart;
    public GameObject buttonOption;
    public AudioSource menuaudio;

    void Start()
    {
        buttonStart.GetComponent<Button>().onClick.AddListener(StartGame);
        buttonOption.GetComponent<Button>().onClick.AddListener(Options);
    }


    public void StartGame()
    {   
        menuaudio.Stop();
        SceneManager.LoadScene("GameScene_MobileApp");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionScene_MobileApp");
    }
}
