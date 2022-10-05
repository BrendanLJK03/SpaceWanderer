using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    public AudioClip EnemyFreezeSound;
    public AudioClip EnemyDieSound;

    [SerializeField] float health, maxHealth = 10f;
    public int damage;
    float damageTime = 0.8f;
    float currentDamageTime;
    bool isDead = false;

    public HPBar healthBar;
    public GameObject BossHP;
    public GameController GameController;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        healthBar.SetMaxHealth(maxHealth);
        
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
                BossHP.SetActive(true);
            }
            if(isInAttackRange)
            {
                rb.velocity = Vector2.zero;
            }
            if(!isInChaseRange)
            {
                BossHP.SetActive(false);
            }
        }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if((controller != null) && (isDead == false))
        {
            currentDamageTime += Time.deltaTime;
            if (currentDamageTime > damageTime)
            {
                controller.ChangeHealth(-1);
                currentDamageTime = 0.0f;
            }
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
            anim.SetBool("isDead", true);
            isDead = true;
            AudioSource.PlayClipAtPoint(EnemyDieSound, Camera.main.transform.position);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            GameController.GameComplete();
        }
        
    }
}
