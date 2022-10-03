using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int Damage = -1;

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
        EnemyAI e2 = other.GetComponent<EnemyAI>();
        PlayerController controller = other.GetComponent<PlayerController>();
        if(controller != null)
        {
            controller.ChangeHealth(Damage);
        }
    }
}