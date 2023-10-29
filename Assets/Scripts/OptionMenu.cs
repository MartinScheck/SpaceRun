using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{

    public GameObject buttonSound;
    public GameObject buttonControlls;
    public GameObject buttonReturn;

    void Start()
    {
        buttonSound.GetComponent<Button>().onClick.AddListener(Sound);
        buttonControlls.GetComponent<Button>().onClick.AddListener(Controlls);
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
    public void Controlls()
    {
        //toDo
    }

}
