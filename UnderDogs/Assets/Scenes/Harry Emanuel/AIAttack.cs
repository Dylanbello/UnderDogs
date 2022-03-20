using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    Ailocomotion aiLocaomotion;
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private float attackDelay = 1;
    [SerializeField] private AudioSource attackSound;
    private float attackTimer;

    private void Awake() { aiLocaomotion = GetComponent<Ailocomotion>(); }

    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent(out DogManager dogManager) && attackTimer < attackDelay) { return; } //Guard clause that stops code unless it detects a player.

        if(dogManager.playerHealth.IsDead)  //If the player is dead, return to patrolling.
        { 
            aiLocaomotion.m_IsPatrol = true; 
            return; 
        }

        //This code runs only if the player isn't dead.
        dogManager.playerHealth.Damage(attackDamage);
        attackSound.Play();
    }

    private void Update() { attackTimer += Time.deltaTime; }
}
