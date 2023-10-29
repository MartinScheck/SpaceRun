using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    private bool onGround;
    public float speed;
    private static int health;
    private static int lives;
    private static int score;

    private static int maxScore;

    private float currentHeroPosition;
    private float oldHeroPosition;

    public HealthBarHUDTester livesbar;
    private Animator anim;
    private int transitionState;
    private Rigidbody2D rb;
    public GameObject respawnPoint;
    public Text healthText, scoreText;
    private bool blockControls;
    void Start()
    {
        oldHeroPosition = 0;
        maxScore = 0;
        score = -5;

        health = 100;
        speed = 10.0f;
        lives = 3;
        blockControls = false;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        onGround = true;
        respawn();
    }

    public void increaseHealth()
    {
        health = health + 10;
        if (health >= 100)
        {
            health = 100;
        }
        Debug.Log("Health is: " + health);
        setHealthColor();
        healthText.text = health + "";
    }

    public void decreaseHealth()
    {
        health = health - 30;
       
        if (health <= 0)
        {
            health = 0;
          
            lives--;
            if(lives <= 0)
            {
                livesbar.Hurt(1);
                lives = 0;
                transitionState = 4;
                anim.SetInteger("Trans", transitionState); // dying animation
                gameOver();
            }
            else
            {
                blockControls = true;
                health = 100;
                setHealthColor();
                healthText.text = health + "";
                Debug.Log("Lives decrease, health restored");
                livesbar.Hurt(1);
               // transitionState = 4;
               // anim.SetInteger("Trans", transitionState); // dying animation
                respawn();
                
            }
            
        }
        else
        {
            Debug.Log("Health reduced");
            setHealthColor();
            healthText.text = health + "";
        }
    }

    private void setHealthColor()
    {
        if(health >= 75)
        {
            healthText.color = Color.green;
        }
        if(health < 75 && health >= 30)
        {
            healthText.color = Color.yellow;
        }
        if(health < 30)
        {
            healthText.color = Color.red;
        }
    }

    private void setScoreColor()
    {
        if (score <= 1000)
        {
            scoreText.color = Color.white;
        }
        if (score < 1000 && score >= 10000)
        {
            scoreText.color = Color.yellow;
        }
        if (score <= 10000 && score >= 100000)
        {
            scoreText.color = Color.cyan;
        }
    }

    public void increaseScore()
    {

        if(maxScore <= currentHeroPosition)
        {
            if(maxScore < 0)
            {
                score = 0;
            }
            else
            {
                if(currentHeroPosition > oldHeroPosition)
                {
                    score = (int)currentHeroPosition;
                    setScoreColor();
                    scoreText.text = score + "";
                    oldHeroPosition = currentHeroPosition;
                }
            }
        }
    }

    public void respawn()
    {
        Debug.Log("RESPAWN");
        anim.SetTrigger("Respawn");
        transform.position = new Vector3(respawnPoint.transform.position.x , respawnPoint.transform.position.y, respawnPoint.transform.position.z);
        transform.localScale = new Vector3(1f, 1f, 1f);
        blockControls = false;
    }
    public void gameOver()
    {
        Debug.Log("GAME OVER");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up") && onGround == true && blockControls == false)
        {

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, 9.0f), ForceMode2D.Impulse);
            onGround = false;
            anim.SetTrigger("Jump"); // jump animation

        }
        if (Input.GetKeyUp("right") || Input.GetKeyUp("left"))
        {
            transitionState = 1;
            anim.SetInteger("Trans", transitionState); // stand animation

        }
        if (Input.GetKeyDown("down") && onGround == true)
        {
            decreaseHealth();
            onGround = true;

        }

    }

    void FixedUpdate()
    {

        if (Input.GetKey("right") && blockControls == false)
        {
            currentHeroPosition = gameObject.transform.position.x;
            increaseScore();
            float distanceToMove = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transitionState = 2;
            anim.SetInteger("Trans", transitionState); // run animation right
        }
        if (Input.GetKey("left") && blockControls == false)
        {
            currentHeroPosition = gameObject.transform.position.x;
            float distanceToMove = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x - distanceToMove, transform.position.y, transform.position.z);
            
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transitionState = 2;
            anim.SetInteger("Trans", transitionState); // run animation left
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       onGround = true;
       transitionState = 1;
       anim.SetInteger("Trans", transitionState); // stand animation

    }

    public int getHealth()
    {
        return health;
    }

    public int getLives()
    {
        return lives;
    }

    public int getScore()
    {
        return score;
    }

}
