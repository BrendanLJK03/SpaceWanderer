using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    [SerializeField] float health, maxHealth = 10f;
    public int damage;
    Rigidbody2D rb;
    Animator anim;

    public HPBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
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

        if(controller != null)
        {
            controller.ChangeHealth(0); //when player touch on the enemy, health will -1
        }
    }
    private void MoveCharacter(Vector2 dir)
        {
            rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
        }

    

    public void TakeDamage()
    {
        health -= damage;
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
