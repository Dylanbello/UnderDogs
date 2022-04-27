using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AI/AIAttack")]
public class AIAttack : MonoBehaviour
{
    Ailocomotion aiLocaomotion;
    private float attackTimer;
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private float attackDelay = 1;
    [SerializeField] private Animator ai_Attack;
    [SerializeField] AudioSource as_attack;
    
    private bool _isDistanceCheck = false;
    
    private void Awake() { aiLocaomotion = GetComponent<Ailocomotion>();}
    
    private void Update(){ attackTimer += Time.deltaTime;}

    private void OnTriggerStay(Collider other)
    {
        DogManager dogManager = other.GetComponent<DogManager>();
        if(!dogManager || attackTimer < attackDelay) {return;}
        
        attackTimer = 0f;

       
        as_attack.Play();
        ai_Attack.SetBool("Attack", true);
        
        dogManager.playerHealth.Damage(attackDamage);

    }

    private void OnTriggerExit(Collider other)
    {
        ai_Attack.SetBool("Attack", false);
    }




}