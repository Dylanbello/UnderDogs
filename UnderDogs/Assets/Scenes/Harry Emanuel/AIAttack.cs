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
public class AIAttack : MonoBehaviour
{  
    [SerializeField] private float attackDamage = 50f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField]private AudioSource attackSound;
    private float canAttack;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("character1") || other.CompareTag("character2")){
            if(attackSpeed <= canAttack){
                other.gameObject.GetComponent<DogsHealth>().UpdateHealth(-attackDamage);
                attackSound.Play();
                canAttack = 0f;
                Debug.Log("Attack Dog");
            }else{
                canAttack += Time.deltaTime;
            }
        }
        else if(other.CompareTag("character1") || other.CompareTag("character2"))
        {
            //TODO: checks the dogs health and if below or equal to 0 return to patrolling
        }
    }
}
