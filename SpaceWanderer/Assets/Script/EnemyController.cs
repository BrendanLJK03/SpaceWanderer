using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;                 //Enemy Speed
    public bool vertical;               //Enemy walking direction
    public float changeTime = 3.0f;     //3,0f is 3 seconds, means enemy will walk to left 3 secs/ to the right 3 secs


    Rigidbody2D rb;
    float timer;
    int direction = 1;

    Animator animator;
    public AudioClip EnemyDieSound;

    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = GetComponent<Rigidbody2D>().position;

        if(vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else 
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        GetComponent<Rigidbody2D>().MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if(controller != null)
        {
            controller.ChangeHealth(-1); //when player touch on the enemy, health will -1
        }
    }

    public void killEnemy()
    {
        AudioSource.PlayClipAtPoint(EnemyDieSound, Camera.main.transform.position);
        Destroy(gameObject);
    }

    public IEnumerator unfreeze()
    {  
        yield return new WaitForSeconds(2);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        animator.GetComponent<Animator>().enabled = true;
    }

    public void freezeEnemy()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        animator.GetComponent<Animator>().enabled = false;
        StartCoroutine(unfreeze());
        AudioSource.PlayClipAtPoint(EnemyDieSound, Camera.main.transform.position);
    }
}
