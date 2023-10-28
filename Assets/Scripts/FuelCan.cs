using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{
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
        if(hs.getHealth() < 100)
        {
            gameObject.SetActive(false);
            hs.increaseHealth();
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

}
