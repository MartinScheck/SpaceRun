using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject gate01;
    public GameObject gate02;
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
        gate01.SetActive(false);
        gate02.SetActive(false);
        //toDo logik für laserwand freischalten für prefab

        hs.playKeyCollectSound();
        gameObject.SetActive(false);
    }

}
