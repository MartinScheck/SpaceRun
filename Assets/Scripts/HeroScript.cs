using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    private bool onGround;
    public float speed;
    private Animator anim;
    private Rigidbody2D rb;

    private static int health;
    private int transitionState;

    public Text healthText;

    void Start()
    {
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        speed = 0.2f;
        onGround = true;
    }

    public int getHealth()
    {
        return health;
    }

    public void increaseHealth()
    {
        health = health + 10;
        if (health >= 100)
        {
            health = 100;
        }
        Debug.Log("Health is: " + health);
        healthText.text = health + "";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("right"))
        {
            transitionState = 2;
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetInteger("Trans", transitionState); //run animation
        }
 
        if (Input.GetKey("left"))
        {
            transitionState = 2;
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(-1f, 1f, 1f);
            anim.SetInteger("Trans", transitionState); //run animation
        }


        if (Input.GetKeyDown("up") && onGround == true)
        {
  
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                // Add a vertical force to the player.
                rb.AddForce(new Vector2(0f, 8.0f), ForceMode2D.Impulse);
                onGround = false;
                anim.SetTrigger("Jump"); //jump animation

        }

        if(Input.GetKeyUp("left") || Input.GetKeyUp("right")) //|| Input.GetKeyUp("up//)
        {
            transitionState = 1;
            anim.SetInteger("Trans", transitionState); //stand animation
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
        transitionState = 1;
        anim.SetInteger("Trans", transitionState); //stand animation

    }
    
}
