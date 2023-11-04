using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
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

    private float scaleSlime = 7.686088f;

    private int randomNumber = 1;


    [SerializeField] private Animator[] EnemyAnims;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.03f;
        anim = GetComponent<Animator>();
        Animation_1_Idle();
        // Setze die Zeit für die nächste Richtungsänderung
        nextChangeTime = Time.deltaTime + Random.Range(10.0f, randomChangeInterval);
        Debug.Log(nextChangeTime);
    }

    void FixedUpdate()
    {

        timer += Time.deltaTime;

        if (timer > randomMaxTimer)
        {
            randomMaxTimer = Random.Range(2f, 10f);
            timer = 0f;

            randomNumber *= -1;

            if (randomNumber < 0)
            {
                gameObject.transform.localScale = new Vector3(-scaleSlime, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(scaleSlime, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }


        }
        else
        {
              Animation_2_Run();
              distanceToMove = speed;
              gameObject.transform.position = new Vector3(gameObject.transform.position.x + (distanceToMove * randomNumber), gameObject.transform.position.y, gameObject.transform.position.z);   
        }

        Debug.Log(timer);
        Debug.Log(randomMaxTimer);

    }

        public void Animation_1_Idle()
    {
        anim.SetBool("Run", false);
    }
    public void Animation_2_Run()
    {
        anim.SetBool("Run", true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("leftWall"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }


        if (collision.gameObject.CompareTag("Hero"))
        {
            Animation_1_Idle();
            anim.SetTrigger("Attack");
            hero.decreaseHealth(5);

        }
        else
        {
            anim.SetBool("Run", true);
        }

    }

}
