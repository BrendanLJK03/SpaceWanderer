using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public int Damage;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    public AudioClip EnemyFreezeSound;
    public float waitTime;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);
    
        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if(shouldRotate)
        {
            anim.SetFloat("Move X", dir.x);
            anim.SetFloat("Move Y", dir.y);
        }
    }
    
    private void FixedUpdate()
        {
            if(isInChaseRange && !isInAttackRange)
            {
                MoveCharacter(movement);
            }
            if(isInAttackRange)
            {
                rb.velocity = Vector2.zero;
            }
        }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Freeze") && controller != null)
        {
            controller.ChangeHealth(0); //when player touch on the enemy, health will -1
        }

        else
        {
            controller.ChangeHealth(Damage);
        }


    }

    
    private void MoveCharacter(Vector2 dir)
        {
            rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
        }

    public IEnumerator unfreeze()
        { 
            yield return new WaitForSeconds(waitTime);
            anim.ResetTrigger("Freeze");
            anim.SetTrigger("Unfreeze");
            anim.GetComponent<Animator>().enabled = true;
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }

    public void Freeze()
        {
            anim.SetTrigger("Freeze");
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Freeze"))
            {
                anim.GetComponent<Animator>().enabled = false;
            }
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            
            StartCoroutine(unfreeze());
            AudioSource.PlayClipAtPoint(EnemyFreezeSound, Camera.main.transform.position);
            
            
        }
}
