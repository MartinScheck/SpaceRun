using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject hero;
    public float distanztoHero;

    public float rotationSpeed = 90.0f; // Geschwindigkeit der Rotation in Grad pro Sekunde
    private float currentRotation = 180.0f; // Startwert bei 180 Grad
    private int rotationDirection = 1; // 1 für Zunahme, -1 für Abnahme

    public GameObject gun;
    private Animator anim;
    public GameObject gunBullet;

    public float firetimedelay;

    // Start is called before the first frame update
    void Start()
    {
        anim = gun.GetComponent<Animator>();
        firetimedelay = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
        distanztoHero = Vector3.Distance(gameObject.transform.position, hero.transform.position);
        if (distanztoHero <= 20.0f)
        {
            GunAiming();
            anim.SetBool("Scope", true);
            GunFire();
        }   
        else 
        {
            
            anim.SetBool("Scope", false);
            GunRotate();
        }
    }
    private void FixedUpdate()
    {
        anim.SetFloat("Firetime",firetimedelay -= Time.deltaTime);
        if(firetimedelay < 1)
        {
            GunFire();
            firetimedelay = 4;
        }
    }
    void GunFire()
    {


        Rigidbody2D rb = gunBullet.GetComponent<Rigidbody2D>();
        // Add a vertical force to the player.
        rb.AddForce(new Vector2(-0.003f, -0.003f).normalized, ForceMode2D.Impulse);


    }

    void GunAiming()
    {
        Vector3 directionToTarget = hero.transform.position - gameObject.transform.position;
        // Berechne den Winkel im Bogenmaß (Radian) zwischen dem aktuellen Objekt und dem Ziel.
        float angleInRadians = Mathf.Atan2(directionToTarget.y, directionToTarget.x);
        // Wandel den Winkel von Radian in Grad um und setze die Z-Rotation des GameObjects.
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angleInDegrees);
    }

    void GunRotate()
    {
        currentRotation += rotationSpeed * rotationDirection * Time.deltaTime;

        // Überprüfe, ob die Rotation 180 Grad oder 360 Grad erreicht hat und ändere die Richtung.
        if (currentRotation >= 360.0f || currentRotation <= 180.0f)
        {
            rotationDirection *= -1;
        }

        // Wende die Rotation auf dein GameObject an.
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);

    }
}
