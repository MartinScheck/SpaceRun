using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpSceneScript_MobileApp : MonoBehaviour
{
    public GameObject backButton;
    // Start is called before the first frame update
    void Start()
    {
        backButton.GetComponent<Button>().onClick.AddListener(BackButton);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("OptionScene_MobileApp");
    }
}
