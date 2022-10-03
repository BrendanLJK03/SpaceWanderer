using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator anim;
    public bool isFrozen;

    void Awake ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 500.0f)
        {
            Destroy(gameObject);
        } 
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        EnemyController e = other.GetComponent<EnemyController>();
        EnemyAI e2 = other.GetComponent<EnemyAI>();
        anim = other.GetComponent<Animator>();
        anim.Play("Froze");

        if(e!=null)
        {
        e. freezeEnemy();
        Destroy(gameObject);
        yield return null;
        }

        else if(e2!=null)
        {
        e2. Freeze();
        Destroy(gameObject);
        yield return null;
        }
    }
}