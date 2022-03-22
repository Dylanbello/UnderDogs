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
        DogManager dogManager = other.GetComponent<DogManager>();
        if(!dogManager || attackTimer < attackDelay) { return; }

        attackTimer = 0;
        dogManager.playerHealth.Damage(attackDamage);
        attackSound.Play();
    }

    private void Update() { attackTimer += Time.deltaTime; }
}
