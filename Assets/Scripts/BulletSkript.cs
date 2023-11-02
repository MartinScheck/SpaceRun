using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkript : MonoBehaviour
{
    private GameObject hero;
    private GameObject heroPosition;
    private Rigidbody2D rb;
    public float bulletForce;
    private float bulletLiveTime;

    public AudioClip bulletSound;
    public AudioSource bulletAudio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        bulletAudio = gameObject.GetComponent<AudioSource>();
        bulletForce = 6f;
        rb = GetComponent<Rigidbody2D>();
        hero = GameObject.FindGameObjectWithTag("Hero");
        heroPosition = GameObject.FindGameObjectWithTag("HeroHitBox");
        Vector3 direction = (heroPosition.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * bulletForce;

    }

    // Update is called once per frame
    void Update()
    {
        bulletLiveTime += Time.fixedDeltaTime;
        if(bulletLiveTime >= 20.0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            HeroScript heroScript = hero.GetComponent<HeroScript>();
            bulletAudio.PlayOneShot(bulletSound);
            heroScript.decreaseHealth(20);
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
       
    }
   

}
