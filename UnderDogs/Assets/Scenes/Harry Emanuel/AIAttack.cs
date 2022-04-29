using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AI/AIAttack")]
public class AIAttack : MonoBehaviour
{
    Ailocomotion aiLocaomotion;
    private float attackTimer = 0f;
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private float attackDelay = 2.2f;
    [SerializeField] private Animator ai_Attack;
    [SerializeField] AudioSource as_attack;

    //For the Attack Function
    bool isAttacking = false;

    DogManager dogManager;

    [Header("SFX Volume")]
    public float attackVolume;
    
    private bool _isDistanceCheck = false;
    
    private void Awake() { aiLocaomotion = GetComponent<Ailocomotion>();}
    
    private void Update()
    {
        //For the attack Function
        if(ai_Attack.GetBool("Attack") == true)
        {
            isAttacking = true;
        }
        if (ai_Attack.GetBool("Attack") == false)
        {
            isAttacking = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        attackTimer += Time.deltaTime;
        dogManager = other.GetComponent<DogManager>();
        if(!dogManager) {return;}

        //For the Attack Function
        if(isAttacking == true && attackTimer >= attackDelay)
        {
            attackTimer = 0f;
        }

        as_attack.Play();
        ai_Attack.SetBool("Attack", true);
        SoundManager.Play3DSound(SoundManager.Sound.EnemyAttack, transform.position, attackVolume);
    }

    private void OnTriggerExit(Collider other)
    {
        ai_Attack.SetBool("Attack", false);
    }

    public DogManager GetDogManager(out float damage)
    {
        damage = attackDamage;
        if (dogManager == null) { return null; }
        else { return dogManager; }
    }
}