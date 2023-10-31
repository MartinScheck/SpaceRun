using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject key;
    public GameObject gate;

    public Text healthText, scoreText;
    private bool blockControls;
    public AudioSource heroAudio;
    public List<AudioClip> heroAudioClip;
    private float time = 0;
    private bool leftfoot = true;
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
        heroAudio = GetComponent<AudioSource>();
        onGround = true;
        respawn();
    }

    public void increaseHealth()
    {
        health = health + 10;
        heroAudio.PlayOneShot(heroAudioClip[6]);
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
        health = health - 10;
        
        
        if (health <= 0)
        {
            health = 0;
            lives--; 
            
            if (lives <= 0)
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
            anim.SetTrigger("Dmg");
            heroAudio.PlayOneShot(heroAudioClip[7]);
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

        activateKey();

    }

    private void activateKey()
    {

        if(score == 200)
        {
            int randomValuex = (int) Random.Range(currentHeroPosition, currentHeroPosition *2);
            float randomValuey = Random.Range(0f, 0.8f);
            activateGate(randomValuex);
            key.transform.position = new Vector3(currentHeroPosition + randomValuex, key.transform.position.y + randomValuey, key.transform.position.z);
            key.SetActive(true);
        }
    }

    private void activateGate(int value)
    {
        float prefabDistance = 25.63582f;
        float gateSpawn = currentHeroPosition + value * 2;
        float traveldPrefabs = gateSpawn / prefabDistance;

        gate.transform.position = new Vector3(Mathf.RoundToInt(traveldPrefabs) * prefabDistance, gate.transform.position.y, gate.transform.position.z);

        gate.SetActive(true);
    }

    public void respawn()
    {
        Debug.Log("RESPAWN");
        anim.SetTrigger("Respawn");
        transform.position = new Vector3(respawnPoint.transform.position.x , respawnPoint.transform.position.y, respawnPoint.transform.position.z);
        transform.localScale = new Vector3(1f, 1f, 1f);
        blockControls = false;
        heroAudio.PlayOneShot(heroAudioClip[5]);
        health = 100;
        healthText.text = health + "";

    }
    public void gameOver()
    {
        heroAudio.PlayOneShot(heroAudioClip[8]);
        health = 0;
        lives = 0;
        SceneManager.LoadScene("GameOverScene");
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
            
            heroAudio.PlayOneShot(heroAudioClip[3]);

        }
        if (Input.GetKeyUp("right") || Input.GetKeyUp("left"))
        {
            //heroAudio.PlayOneShot(heroAudioClip[2]);
            transitionState = 1;
            anim.SetInteger("Trans", transitionState); // stand animation

        }
        if (Input.GetKeyDown("down") && onGround == true)
        {
            heroAudio.PlayOneShot(heroAudioClip[4]);
            
            onGround = true;

        }

    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;

        if (Input.GetKey("right") && blockControls == false)
        {
            currentHeroPosition = gameObject.transform.position.x;
            increaseScore();
            float distanceToMove = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transitionState = 2;
            anim.SetInteger("Trans", transitionState); // run animation right
            
            if (time >= 0.5f && onGround == true)
            {
                Footstepsound();
                time = 0;
            }
        }
        if (Input.GetKey("left") && blockControls == false)
        {

            currentHeroPosition = gameObject.transform.position.x;

            float distanceToMove = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x - distanceToMove, transform.position.y, transform.position.z);
            
            transform.localScale = new Vector3(-1f, 1f, 1f);
            transitionState = 2;
            anim.SetInteger("Trans", transitionState); // run animation left

            if (time >= 0.5f && onGround == true)
            {
                Footstepsound();
                time = 0;
            }
        }
        
    }

    private void Footstepsound()
    {
        if (leftfoot)
        {
            heroAudio.PlayOneShot(heroAudioClip[0]);
            leftfoot = false;
        }
        else
        {
            heroAudio.PlayOneShot(heroAudioClip[1]);
            leftfoot = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       onGround = true;
       transitionState = 1;
       anim.SetInteger("Trans", transitionState); // stand animation
       //heroAudio.PlayOneShot(heroAudioClip[2]);

    }

    public void playKeyCollectSound()
    {
        heroAudio.PlayOneShot(heroAudioClip[9]);
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
