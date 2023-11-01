using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    public GameObject buttonSound;
    public GameObject buttonHelp;
    public GameObject buttonReturn;



    void Start()
    {
        buttonSound.GetComponent<Button>().onClick.AddListener(Sound);
        buttonHelp.GetComponent<Button>().onClick.AddListener(Help);
        buttonReturn.GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
    }


    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Sound()
    {
        //toDo
    }
    public void Help()
    {
        SceneManager.LoadScene("HelpSceneComputer");
    }

}
