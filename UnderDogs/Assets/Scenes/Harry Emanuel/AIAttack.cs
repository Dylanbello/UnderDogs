using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    Ailocomotion aiLocaomotion;
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private float attackDelay = 1;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private Animator ai_Attack;
    private bool _isDistanceCheck = false;
    

    private float attackTimer;

    private void Awake() { aiLocaomotion = GetComponent<Ailocomotion>();}

    private void OnTriggerStay(Collider other)
    {
        DogManager dogManager = other.GetComponent<DogManager>();
        if(!dogManager || attackTimer < attackDelay) { return; }

        attackTimer = 0;
        dogManager.playerHealth.Damage(attackDamage);
        attackSound.Play();
        
        if(dogManager == true)
        {
            ai_Attack.SetBool("Attack",true);
        }
        else
        {
            ai_Attack.SetBool("Attack",false);
        }
        
    }

    private void Update() { attackTimer += Time.deltaTime; }
}
