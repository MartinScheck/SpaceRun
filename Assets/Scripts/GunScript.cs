using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject hero;
    
 
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 directionToTarget = hero.transform.position - gameObject.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        gameObject.transform.rotation = targetRotation;*/

        // Berechne die Richtung vom sourceObject zum targetObject.
        Vector3 directionToTarget = hero.transform.position - gameObject.transform.position;

        // Berechne den Winkel zwischen der aktuellen Richtung und der Richtung zum Ziel.
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Rotiere das GameObject in Richtung des Ziels.
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


}
