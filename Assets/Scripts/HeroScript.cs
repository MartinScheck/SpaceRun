using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroScript : MonoBehaviour
{
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
    public GameObject gateBlocker;

    public Text healthText, scoreText;
    private bool blockControls;
    public AudioSource heroAudio;
    public List<AudioClip> heroAudioClip;
    private float time = 0;
    private bool leftfoot = true;

    private bool respawned;
    private bool gameover = false;
    private float gameovertimer = 5.0f;

    public Joystick joystick;

    private GroundCheckSkript groundcheck;

    public float currentspeed = 0;
    float previousHeroPosition;

    void Start()
    {
        respawned = false;
        rb = GetComponent<Rigidbody2D>();
        oldHeroPosition = 0;
        maxScore = 0;
        score = -5;

        health = 100;
        speed = 8.0f;
        lives = 3;
        blockControls = false;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        heroAudio = GetComponent<AudioSource>();
        groundcheck = GetComponentInChildren<GroundCheckSkript>();
        previousHeroPosition = gameObject.transform.position.x;


        respawn();
        respawned = false;
    }

    public void increaseHealth()
    {
        health = health + 10;
        heroAudio.PlayOneShot(heroAudioClip[6]);
        if (health >= 100)
        {
            health = 100;
        }
        setHealthColor();
        healthText.text = health + "";
    }

    public void decreaseHealth(int dmg = 10)
    {
        if (!gameover)
        {
            if (!respawned)
            {
                health = health - dmg;
            }
            else
            {
                health = 100;
                respawned = false;
            }


            if (health <= 0)
            {
                health = 0;
                lives--;

                if (lives <= 0)
                {
                    livesbar.Hurt(1);
                    lives = 0;
                    transitionState = 4;
                    anim.SetTrigger("Fall"); // dying animation
                    gameOver();
                }
                else
                {
                    blockControls = true;
                    health = 100;
                    setHealthColor();
                    healthText.text = health + "";
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

        if(score == 150)
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

        float gatePosition = Mathf.RoundToInt(traveldPrefabs) * prefabDistance;

        gate.transform.position = new Vector3(gatePosition, -1f, gate.transform.position.z);
        gateBlocker.transform.position = new Vector3(gatePosition + 1.6437f, -1f, gateBlocker.transform.position.z);
        gateBlocker.SetActive(true);
        gate.SetActive(true);
    }

    public void respawn()
    {

        respawned = true;
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
        blockControls = true;
        gameover = true;
        Debug.Log("GAME OVER");   
    }

    private void GameendScene()
    {
        if (SceneManager.GetActiveScene().name == "GameScene_App")
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            SceneManager.LoadScene("GameOverScene_MobileApp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (groundcheck.IsGrounded() == false)
        {
            anim.SetBool("canJump",true);
        }
        else
        {
            anim.SetBool("canJump", false);
            //anim.SetInteger("Trans", 1);
        }

        if (Input.GetKeyDown("up") && blockControls == false && groundcheck.IsGrounded())
        {
            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, 9.0f), ForceMode2D.Impulse);
            
            //anim.SetTrigger("Jump"); // jump animation
            anim.SetBool("canJump", true);

            heroAudio.PlayOneShot(heroAudioClip[3]);

        }

        if (Input.GetKeyDown("down") )
        {
            heroAudio.PlayOneShot(heroAudioClip[4]);
        }

    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;
        previousHeroPosition = currentHeroPosition;
        currentHeroPosition = gameObject.transform.position.x;
        currentspeed = Mathf.Abs((currentHeroPosition - previousHeroPosition) / Time.deltaTime);
        anim.SetFloat("speed", currentspeed);
        Debug.Log(currentspeed);

        if (currentspeed <= 0.07f && groundcheck.IsGrounded())
        {
            transitionState = 1;
            anim.SetInteger("Trans", transitionState);
        }

        if (Input.GetKey("right") && blockControls == false)
        {        
            currentHeroPosition = gameObject.transform.position.x;
            increaseScore();
            float distanceToMove = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(1f, 1f, 1f);

            if (groundcheck.IsGrounded())
            {
                transitionState = 2;
                anim.SetInteger("Trans", transitionState); // run animation right
            }
            
            
            if (time >= 0.5f && groundcheck.IsGrounded())
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
            if (groundcheck.IsGrounded())
            {
                transitionState = 2;
                anim.SetInteger("Trans", transitionState); // run animation right
            }

            if (time >= 0.5f && groundcheck.IsGrounded())
            {
                Footstepsound();
                time = 0;
            }
        }


        if (!blockControls && joystick.isActiveAndEnabled)
        {
            float horizontalInput = joystick.Horizontal; // Joystick-Eingabe f�r Links/Rechts
            float verticalInput = joystick.Vertical;
            float distanceToMove = horizontalInput * speed * Time.deltaTime;
            

            if (horizontalInput != 0)
            {
                
                if (horizontalInput > 0)
                {
                    currentHeroPosition = gameObject.transform.position.x;
                    transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                    transform.localScale = new Vector3(1f, 1f, 1f); // Held schaut nach rechts
                    if (groundcheck.IsGrounded())
                    {   
                        increaseScore();
                        transitionState = 2;
                        anim.SetInteger("Trans", transitionState); // run animation right
                    }
                }
                else if (horizontalInput < 0)
                {
                    currentHeroPosition = gameObject.transform.position.x;
                    transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                    transform.localScale = new Vector3(-1f, 1f, 1f); // Held schaut nach links
                    if (groundcheck.IsGrounded())
                    {
                        transitionState = 2;
                        anim.SetInteger("Trans", transitionState); // run animation right
                    }
                }
                
                if (time >= 0.5f && groundcheck.IsGrounded())
                {
                    Footstepsound();
                    time = 0;
                }
            }

            if (verticalInput > 0.1f && groundcheck.IsGrounded())
            {
                Debug.Log(verticalInput);
                rb.AddForce(new Vector2(0f, 9.0f), ForceMode2D.Impulse);

                anim.SetBool("canJump", true);

                heroAudio.PlayOneShot(heroAudioClip[3]);
            }

        }
        if (gameover)
        {
            gameovertimer = gameovertimer - Time.deltaTime;
            if(gameovertimer<= 0)
            {
                GameendScene();
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


    public void playKeyCollectSound()
    {
        heroAudio.PlayOneShot(heroAudioClip[9]);
    }

    public void playGunBullesSound()
    {
        heroAudio.PlayOneShot(heroAudioClip[10]);
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

    public bool getRespawned()
    {
        return respawned;
    }

    public void setRespawned()
    {
        respawned = false;
    }
}
