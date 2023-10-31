using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefabCloned : MonoBehaviour
{
    public GameObject light_L01;
    public GameObject light_L02;
    public GameObject light_L03;
    public GameObject light_L04;

    public GameObject light01_W01;
    public GameObject light02_W01;
    public GameObject light03_W01;
    public GameObject light04_W01;
    public GameObject light05_W01;
    public GameObject light06_W01;
    public GameObject light07_W01;
    public GameObject light08_W01;

    public GameObject light01_W02;
    public GameObject light02_W02;
    public GameObject light03_W02;
    public GameObject light04_W02;
    public GameObject light05_W02;
    public GameObject light06_W02;
    public GameObject light07_W02;
    public GameObject light08_W02;


    private float updateInterval = 0.5f;
    private float lastUpdateTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            DeactivateOrActivateLight();
            lastUpdateTime = Time.time;
        }
    }

    private bool random()
    {
        int randomeValue = Random.Range(0, 2);

        if (randomeValue == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void DeactivateOrActivateLight()
    {

        light_L01.SetActive(random());

        light_L02.SetActive(random());

        light_L03.SetActive(random());

        light_L04.SetActive(random());

        light01_W01.SetActive(random());

        light02_W01.SetActive(random());

        light03_W01.SetActive(random());

        light04_W01.SetActive(random());

        light05_W01.SetActive(random());

        light06_W01.SetActive(random());

        light07_W01.SetActive(random());

        light08_W01.SetActive(random());


        light01_W02.SetActive(random());

        light02_W02.SetActive(random());

        light03_W02.SetActive(random());

        light04_W02.SetActive(random());

        light05_W02.SetActive(random());

        light06_W02.SetActive(random());

        light07_W02.SetActive(random());

        light08_W02.SetActive(random());

    }
}
