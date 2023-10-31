using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject gate;
    public HeroScript hs;

    public bool keyCollected;

    // Start is called before the first frame update
    void Start()
    {
        keyCollected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gate.SetActive(false);
        //toDo logik für laserwand freischalten für prefab

        hs.playKeyCollectSound();
        keyCollected = true;
        gameObject.SetActive(false);
    }

    public bool getKeyCollected()
    {
        return keyCollected;
    }
}
