using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkript : MonoBehaviour
{
    private GameObject hero;
    private Rigidbody2D rb;
    public float bulletForce;
    private float bulletLiveTime;

    public AudioClip bulletSound;
    public AudioSource bulletAudio;

    // Start is called before the first frame update
    void Start()
    {
        bulletAudio = gameObject.GetComponent<AudioSource>();
        bulletForce = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        hero = GameObject.FindGameObjectWithTag("Hero");

        Vector3 direction = hero.transform.position - transform.position;
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
