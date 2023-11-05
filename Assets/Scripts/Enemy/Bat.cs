using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

    public HeroScript hero;

    private Animator anim;

    private float timer = 0;
    private float randomMaxTimer = 5f;
    private float speed;
    public float currentspeed = 0;
    private float randomChangeInterval = 20.0f; // Intervall, in dem die Richtung geändert wird
    private float nextChangeTime = 0.0f;
    public float distanceToMove;


    [SerializeField] private Animator[] EnemyAnims;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;
        anim = GetComponent<Animator>();
        Animation_1_Idle();
        // Setze die Zeit für die nächste Richtungsänderung
        nextChangeTime = Time.deltaTime + Random.Range(10.0f, randomChangeInterval);
    }

    void FixedUpdate()
    {

        timer += Time.deltaTime;

        if (timer > randomMaxTimer)
        {
            randomMaxTimer = Random.Range(2f, 5f);
            timer = 0f;

        }
        else
        {
              Animation_2_Run();
              distanceToMove = speed;
              gameObject.transform.position = new Vector3(gameObject.transform.position.x - (distanceToMove), gameObject.transform.position.y, gameObject.transform.position.z);   
        }

    }

        public void Animation_1_Idle()
    {
        anim.SetBool("Run", false);
    }
    public void Animation_2_Run()
    {
        anim.SetBool("Run", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("leftWall"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        if (other.gameObject.CompareTag("Hero"))
        {
            Animation_1_Idle();
            anim.SetTrigger("Attack");
            hero.decreaseHealth(30);
        }
        else
        {
            anim.SetBool("Run", true);
        }

    }

}
