using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatorPrefabMoving : MonoBehaviour
{

    public GameObject sensor1;
    public GameObject sensor2;
    public GameObject sensor3;
    public GameObject sensor4;


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
    }

}
