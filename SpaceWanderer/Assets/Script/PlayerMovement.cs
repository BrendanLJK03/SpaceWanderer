using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 2f;
    private Rigidbody2D rb;
    Animator animator;
    private Vector2 move;
    
    float horizontal;
    float vertical;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        animator.SetFloat("Look X", move.x);
        animator.SetFloat("Look Y", move.y);
    }

    void FixedUpdate()
    {
        rb.velocity = move * movementSpeed;
    }
}
