using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public int health;
    public int MaxHealth;

    Rigidbody2D rb;
    Animator anim;

    public Image BossHP;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, MaxHealth);
        BossHP.fillAmount = (float)health / (float)MaxHealth;
    }
}
