using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject buttonStart;
    public GameObject buttonOption;

    void Start()
    {
        buttonStart.GetComponent<Button>().onClick.AddListener(StartGame);
        buttonOption.GetComponent<Button>().onClick.AddListener(Options);
    }


    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionScene");
    }
}
