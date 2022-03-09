using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    THIS IS A APA7TH SCRIPT/CODE from https://uark.libguides.com/CSCE/CitingCode
Title: AI Health
Aurther: Brackeys
Date: <2020>
Availability https://youtu.be/ieyHlYp5SLQ
*/
public class AIHealth : MonoBehaviour
{
    public int maxHeatlh = 100;
    [HideInInspector]
    public int currentHealth;
    public AIUIHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHeatlh;
    }

    void Update()
    {
    
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth<= 0)
        {
            Die();
        }
    }

    void Die(){
        //Wait for Delay Destory Object 
        //Play Particles
        //Play animation
        Destroy(this.gameObject);
    }
}
