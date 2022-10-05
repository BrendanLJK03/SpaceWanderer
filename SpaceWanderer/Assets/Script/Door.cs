using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isInRange;
    Animator anim;
    public LayerMask whatIsPlayer;
    public AudioClip DoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isInRange", isInRange);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isInRange = true;
            AudioSource.PlayClipAtPoint(DoorOpen, Camera.main.transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            isInRange = false;
    }
}
