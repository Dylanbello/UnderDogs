using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogsHealth : MonoBehaviour
{
    [SerializeField]private float health = 0f;
    [SerializeField] private float maxHealth = 100f;

    void Start()
    {
        health = maxHealth;
    }

    public void UpdateHealth(float mod)
    {
        health += mod;
        if(health > maxHealth){
            health = maxHealth;
        }else if(health <= 0f){
            health = 0f;
            Debug.Log("Player is Dead");
        }
    }
}
