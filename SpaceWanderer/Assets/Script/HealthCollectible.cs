using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip HPSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        PlayerController controller = other.GetComponent<PlayerController>();
    
    if(controller!= null)
    {
        if (controller.currentHealth < controller.maxHealth)
        {
            AudioSource.PlayClipAtPoint(HPSound, Camera.main.transform.position);
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
    }
}
