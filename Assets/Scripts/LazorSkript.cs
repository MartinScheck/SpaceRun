using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazorSkript : MonoBehaviour
{
    public HeroScript hero;
    private float time;

    // Start is called before the first frame update
    void Start()
    { 
    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        time = time + Time.deltaTime;
        Debug.Log(time);
            
        hero.decreaseHealth();
        
       
    }

}
