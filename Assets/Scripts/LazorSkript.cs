using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorSkript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    { 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(hero.getHeroDmgOn())
        {
            
            hero.decreaseHealth();
        }
         
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (hero.getHeroDmgOn())
        {

            hero.decreaseHealth();
        }
    }

}
