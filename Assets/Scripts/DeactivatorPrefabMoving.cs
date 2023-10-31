using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatorPrefabMoving : MonoBehaviour
{

    public GameObject sensor1;
    public GameObject sensor2;
    public GameObject sensor3;
    public GameObject sensor4;

    public GameObject sensorCloned1;
    public GameObject sensorCloned2;
    public GameObject sensorCloned3;
    public GameObject sensorCloned4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        sensor1.SetActive(false);
        sensor2.SetActive(false);
        sensor3.SetActive(false);
        sensor4.SetActive(false);

        sensorCloned1.SetActive(true);
        sensorCloned2.SetActive(true);
        sensorCloned3.SetActive(true);
        sensorCloned4.SetActive(true);

    }


}
