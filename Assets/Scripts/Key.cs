using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject gate;
    public HeroScript hs;

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
        gate.SetActive(false);
        //toDo logik f�r laserwand freischalten f�r prefab

        hs.playKeyCollectSound();
        gameObject.SetActive(false);
    }

}
