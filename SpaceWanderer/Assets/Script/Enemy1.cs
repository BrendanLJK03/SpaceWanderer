using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform character;
    public float speed;
    public bool vertical;
    public string name;
    private Vector2 movement;

    private Rigidbody2D rb;
    Animator animator;
    public AudioClip EnemyDieSound;

    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = character.position - transform.position;
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate() 
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction) {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public IEnumerator unfreeze()
    {  
        yield return new WaitForSeconds(2);
        rb.constraints = RigidbodyConstraints2D.None;
        animator.GetComponent<Animator>().enabled = true;
    }

    public void freezeEnemy()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        animator.GetComponent<Animator>().enabled = false;
        StartCoroutine(unfreeze());
        AudioSource.PlayClipAtPoint(EnemyDieSound, Camera.main.transform.position);
    }
}
