using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AI/AIAttack")]
public class AIAttack : MonoBehaviour
{
    Ailocomotion aiLocomotion;
    public DogManager dogManager;
    private float attackTimer;
    [SerializeField] private int attackDamage = 50;
    [SerializeField] private float attackDelay = 1;
    [SerializeField] private Animator ai_Attack;
    [SerializeField] AudioSource s_attack;
    
    private void Awake() 
    { 
        aiLocomotion = GetComponentInParent<Ailocomotion>();
        s_attack = GetComponent<AudioSource>();
    }

    private void Update()
    { 
        attackTimer += Time.deltaTime;
        if(aiLocomotion.m_PlayerNear == false)
        {
            ai_Attack.SetBool("Attack", false);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(!other.TryGetComponent(out dogManager))
        {
            ai_Attack.SetBool("Attack", false);
            s_attack.Stop();
        }
        /*
        if(dogManager.playerHealth.IsDead)
        {
            Debug.Log("if the fucking enemy is still attacking im done");
        }
        */
        if(!dogManager || attackTimer < attackDelay) {return;}
        
        attackTimer = 0;

        s_attack.Play();
        dogManager.playerHealth.Damage(attackDamage);
        ai_Attack.SetBool("Attack", true);

        dogManager=null;
    }
}
