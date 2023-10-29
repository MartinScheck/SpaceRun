using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorSkript : MonoBehaviour
{
    public HeroScript hero;

    // Start is called before the first frame update
    void Start()
    { 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(hero.getHeroDmgOn())
        {
            //hero.decreaseHealth();
        }
         
    }
}
