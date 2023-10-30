using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float correctionPosition;
    public float currentGunAngle;
    private float differenz;
    public float gunPosX;
    public float gunPosY;
 
    // Start is called before the first frame update
    void Start()
    {
        correctionPosition = 0.013f;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (currentGunAngle != getGunAngle())
        {
            gunPosX = gameObject.transform.position.x;
            gunPosY = gameObject.transform.position.y;
            differenz = Mathf.Abs(currentGunAngle) - Mathf.Abs(getGunAngle());
            
            if(differenz < 0)
            {
                gunPosX = gunPosX + (differenz * correctionPosition) ;
                gunPosY =  gunPosY - (differenz * correctionPosition);
                gameObject.transform.position = new Vector3(gunPosX, gunPosY, gameObject.transform.position.z);

            }
            else
            {
                gunPosX = gunPosX - (differenz * correctionPosition);
                gunPosY = gunPosY + (differenz * correctionPosition);
                gameObject.transform.position = new Vector3(gunPosX, gunPosY, gameObject.transform.position.z);
            }
            


        }
        currentGunAngle = getGunAngle();
    }

    public void RotateGun()
    {
        
    }

    public float getGunAngle()
    {
        return gameObject.transform.rotation.eulerAngles.z;
    }
    
}
