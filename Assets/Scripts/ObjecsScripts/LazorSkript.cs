using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorSkript : MonoBehaviour
{
    public HeroScript hero;
    private float dmgdelaytime = 0.5f;
    private bool dmgOn = false;
    private float timeSinceLastDamage = 0.0f;
    public AudioSource lazerAudioSource;
    public AudioClip lazerclip;


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
                lazerAudioSource.PlayOneShot(lazerclip);
  
                if (!hero.getRespawned())
                {
                    hero.decreaseHealth();
                    timeSinceLastDamage = 0.0f;
                }
                else
                {
                    hero.setRespawned();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            dmgOn = true;
            lazerAudioSource.PlayOneShot(lazerclip);
        }
    }
  
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            dmgOn = false;
            if (!hero.getRespawned())
            {
                hero.decreaseHealth();
                timeSinceLastDamage = 0.0f;
            }

        }
    }

}
