using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroScript : MonoBehaviour
{
    public float speed;
    private float crouchspeed;
    public float normalspeed;
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
    private CrouchCheckScript crouchCheckScript;
    private CapsuleCollider2D upperCollider;

    public float currentspeed = 0;
    public float previousHeroPosition;
    private bool iscrouching = false;

    private float colliderYsizeSmall = 1.987017f;
    private float colliderYoffsetSmall = -1.710181f;

    private float colliderYsizeNormal = 3.468915f;
    private float colliderYoffsetNormal = -0.9672953f;

    private bool audiotimer;
    private float audiotimerdelay = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blockControls = false;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        heroAudio = GetComponent<AudioSource>();
        groundcheck = GetComponentInChildren<GroundCheckSkript>();
        crouchCheckScript = GetComponentInChildren<CrouchCheckScript>();
        previousHeroPosition = gameObject.transform.position.x;
        upperCollider = GetComponent<CapsuleCollider2D>();
        
        
       
        oldHeroPosition = 0;
        maxScore = 0;
        score = -5;
        health = 100;
        speed = 5.0f;
        normalspeed = speed;
        crouchspeed = speed / 2;
        lives = 3;
        respawn();
        respawned = false;
    }

    public void increaseHealth()
    {
        health = health + 10;
        playclipVolumes(6);
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
                if (health % 5 == 0)
                {
                    playclipVolumes(7);
                }
                anim.SetTrigger("Dmg");
                setHealthColor();
                healthText.text = health + "";
            }
        }
    }

    private void setHealthColor()
    {
        if (health >= 75)
        {
            healthText.color = Color.green;
        }
        if (health < 75 && health >= 30)
        {
            healthText.color = Color.yellow;
        }
        if (health < 30)
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

        if (maxScore <= currentHeroPosition)
        {
            if (maxScore < 0)
            {
                score = 0;
            }
            else
            {
                if (currentHeroPosition > oldHeroPosition)
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

        if (score == 200)
        {
            int randomValuex = (int)Random.Range(currentHeroPosition, currentHeroPosition);
            activateGate(randomValuex);
            key.transform.localPosition = new Vector3(currentHeroPosition + randomValuex, key.transform.position.y, key.transform.position.z);
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
        transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, respawnPoint.transform.position.z);
        transform.localScale = new Vector3(1f, 1f, 1f);
        blockControls = false;
        playclipVolumes(5);
        health = 100;
        healthText.text = health + "";
    }

    public void gameOver()
    {
        playclipVolumes(8);
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


    void Update()
    {
        if (groundcheck.IsGrounded() == false && crouchCheckScript.CanNotStand() == false && iscrouching == false)
        {
            anim.SetBool("canJump", true);
        }
        else
        {
            anim.SetBool("canJump", false);
        }

        if (!joystick.isActiveAndEnabled)
        {
            if (Input.GetKeyDown("up") && blockControls == false && groundcheck.IsGrounded() && crouchCheckScript.CanNotStand() == false)
            {
                rb.AddForce(new Vector2(0f, 9.0f), ForceMode2D.Impulse);
                anim.SetBool("canJump", true);
                playclipVolumes(3);
            }

            if (Input.GetKeyDown("down") && groundcheck.IsGrounded())
            {
                speed = crouchspeed;
                playclipVolumes(4);
                setCollidersmall(true);
                anim.SetBool("crouch", true);
                anim.SetBool("canJump", false);
                iscrouching = true;
            }

            if (!Input.GetKey("down"))
            {
                if (crouchCheckScript.CanNotStand() == false)
                {
                    setCollidersmall(false);
                    speed = normalspeed;
                    anim.SetBool("crouch", false);
                    iscrouching = false;


                }
                else
                {
                    setCollidersmall(true);
                    anim.SetBool("crouch", true);
                }

            }
        }
    }

    void FixedUpdate()
    {
        time = time + Time.deltaTime;
        previousHeroPosition = currentHeroPosition;
        currentHeroPosition = gameObject.transform.position.x;
        currentspeed = Mathf.Abs((currentHeroPosition - previousHeroPosition) / Time.deltaTime);
        anim.SetFloat("speed", currentspeed);

        if (currentspeed <= 0.07f && groundcheck.IsGrounded())
        {
            transitionState = 1;
            anim.SetInteger("Trans", transitionState);
        }
        if (!joystick.isActiveAndEnabled)
        {
            if (Input.GetKey("right") && blockControls == false)
            {
                currentHeroPosition = gameObject.transform.position.x;
                increaseScore();
                float distanceToMove = speed * Time.deltaTime;
                transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(1f, 1f, 1f);

                if (groundcheck.IsGrounded() && iscrouching == false)
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
                if (groundcheck.IsGrounded() && iscrouching == false)
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

            if(audiotimer == true)
            {
                audiotimerdelay = audiotimerdelay - Time.deltaTime;
                audiotimerdelay = 5;
            }
        }


        if (!blockControls && joystick.isActiveAndEnabled)
        {
            float horizontalInput = joystick.Horizontal; // Joystick-Eingabe für Links/Rechts
            float verticalInput = joystick.Vertical;
            float distanceToMove = horizontalInput * speed * Time.deltaTime;


            if (horizontalInput != 0)
            {

                if (crouchCheckScript.CanNotStand() == false)
                {
                    setCollidersmall(false);
                    speed = normalspeed;
                    anim.SetBool("crouch", false);
                    iscrouching = false;


                }
                else
                {
                    setCollidersmall(false);
                    anim.SetBool("crouch", true);
                }

                if (horizontalInput > 0)
                {
                    currentHeroPosition = gameObject.transform.position.x;
                    transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
                    transform.localScale = new Vector3(1f, 1f, 1f); // Held schaut nach rechts
                    if (groundcheck.IsGrounded() && iscrouching == false)
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
                    if (groundcheck.IsGrounded() && iscrouching == false)
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

            if (verticalInput > 0.4f && groundcheck.IsGrounded() && crouchCheckScript.CanNotStand() == false)
            {
                Debug.Log(verticalInput);
                rb.AddForce(new Vector2(0f, 9.0f), ForceMode2D.Impulse);

                anim.SetBool("canJump", true);
                playclipVolumes(3);

            }

            if (verticalInput < -0.4f && iscrouching == false && groundcheck.IsGrounded())
            {
                speed = crouchspeed;
                setCollidersmall(true);
                anim.SetBool("crouch", true);
                anim.SetBool("canJump", false);
                iscrouching = true;
            }
            if(verticalInput >= -0.4f && verticalInput <= 0.4f)
            {
                if (crouchCheckScript.CanNotStand() == false)
                {
                    setCollidersmall(false);
                    speed = normalspeed;
                    anim.SetBool("crouch", false);
                    iscrouching = false;


                }
                else
                {
                    setCollidersmall(true);
                    anim.SetBool("crouch", true);
                }
            }

        }
        if (gameover)
        {
            gameovertimer = gameovertimer - Time.deltaTime;
            if (gameovertimer <= 0)
            {
                GameendScene();
            }
        }


    }

    private void Footstepsound()
    {
        if (leftfoot)
        {
            playclipVolumes(0);
            leftfoot = false;
        }
        else
        {
            playclipVolumes(1);
            leftfoot = true;
        }
    }


    public void playKeyCollectSound()
    {
        playclipVolumes(9);
    }

    public void playGunBullesSound()
    {
        playclipVolumes(10);
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

    public void setCollidersmall(bool on)
    {
        if(on)
        {
            upperCollider.size = new Vector2(upperCollider.size.x, colliderYsizeSmall);
            upperCollider.offset = new Vector2(upperCollider.offset.x, colliderYoffsetSmall);
        }
        else
        {
            upperCollider.size = new Vector2(upperCollider.size.x, colliderYsizeNormal);
            upperCollider.offset = new Vector2(upperCollider.offset.x, colliderYoffsetNormal);
        }
    }

    public void playclipVolumes(int clipNumber )
    {
        float stepsound = 0.2f;
        switch (clipNumber)
        {
            case 0:
                heroAudio.volume = stepsound; // run left
                break;
            case 1:
                heroAudio.volume = stepsound; // run right
                break;
            case 2:
                heroAudio.volume = stepsound; // jump start
                break;
            case 3:
                heroAudio.volume = 0.05f; // jump (mario)
                break;
            case 4:
                heroAudio.volume = stepsound; // jump land
                break;
            case 5:
                heroAudio.volume = 0.05f; // respawn
                break;
            case 6:
                heroAudio.volume = 0.2f; // live increase
                break;
            case 7:
                heroAudio.volume = 0.2f; // dmg (live deacrease)
                break;
            case 8:
                heroAudio.volume = 1.0f; // gameover
                break;
            case 9:
                heroAudio.volume = 0.7f; // key
                break;
            case 10:
                heroAudio.volume = 1.0f; // bullethitsound
                break;
            default:
                heroAudio.volume = 1.0f;
                break;
        }


        heroAudio.PlayOneShot(heroAudioClip[clipNumber]);
    }
}
