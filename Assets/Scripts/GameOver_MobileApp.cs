using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver_MobileApp : MonoBehaviour
{
    public Text textComponent;
    private float blinkInterval = 1.0f;
    private bool isTextVisible = true;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = blinkInterval;
    }

    // Update is called once per frame
    private void Update()
    {

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            isTextVisible = !isTextVisible;
            textComponent.enabled = isTextVisible;
            timer = blinkInterval;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Annahme: Nur die erste Berührung wird berücksichtigt
            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("GameScene_MobileApp");
            }
        }
    }
}