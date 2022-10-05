using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    Rigidbody2D rigidbody2D;
    private Vector2 move;
    [SerializeField] private float movementSpeed = 2f;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;

    public AudioClip projectileSound;

    public Image HPBar;
    public GameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(Mathf.Approximately(move.x,0.0f)|| !Mathf.Approximately(move.y,0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();   
        }

        if(currentHealth <= 0)
        {
            GameController.GameOver();
        }
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = move * movementSpeed;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        HPBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

        AudioSource.PlayClipAtPoint(projectileSound, Camera.main.transform.position);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
    }
}
