using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject buttonStart;

    void Start()
    {
        buttonStart.GetComponent<Button>().onClick.AddListener(StartGame);
    }


    public void StartGame()
    {
        Debug.Log("ButtonPressed");
        SceneManager.LoadScene("GameScene");
    }
}
