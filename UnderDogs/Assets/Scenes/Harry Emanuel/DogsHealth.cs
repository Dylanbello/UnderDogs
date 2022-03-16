using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
//THIS IS A APA7TH SCRIPT/CODE from https://uark.libguides.com/CSCE/CitingCode
Title: AIAttacking
Aurther: Muddy Wolf
Date: <2021>
Availability https://youtu.be/VOdYtqV_meo
*/
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
            //TODO: Destory or play death sequence
        }
    }
}
