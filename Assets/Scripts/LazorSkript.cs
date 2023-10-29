using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorSkript : MonoBehaviour
{
    public HeroScript hero;
    private float dmgdelaytime = 0.5f;
    private bool dmgOn = false;
    private float timeSinceLastDamage = 0.0f;

    // Start is called before the first frame update
    void Start()
    { 
    }

    private void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if (dmgOn)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= dmgdelaytime)
            {
                hero.decreaseHealth();
                timeSinceLastDamage = 0.0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            dmgOn = true;
        }
    }
  
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            dmgOn = false;
            timeSinceLastDamage = 0.0f;
            hero.decreaseHealth();

        }
    }

}
