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
  
    // Start is called before the first frame update
    void Start()
    {
        bulletForce = 1.5f;
        rb = GetComponent<Rigidbody2D>();
        hero = GameObject.FindGameObjectWithTag("Hero");
        heroPosition = GameObject.FindGameObjectWithTag("HeroHitBox");
        Vector3 direction = heroPosition.transform.position - transform.position;
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
            heroScript.decreaseHealth(20);
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
       
    }
   

}
