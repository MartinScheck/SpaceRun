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
        Vector3 directionToTarget = hero.transform.position - gameObject.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        gameObject.transform.rotation = targetRotation;
    }


}
