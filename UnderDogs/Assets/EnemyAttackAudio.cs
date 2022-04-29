using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAudio : MonoBehaviour
{
    public float attackVolume;
    public bool attack = false;

    [SerializeField] AIAttack ai_Attack;


    public void AttackSound()
    {
        DogManager target = ai_Attack.GetDogManager(out float attackDamage);

        if(target != null) { target.playerHealth.Damage((int)attackDamage); }

        SoundManager.Play3DSound(SoundManager.Sound.EnemyAttack, transform.position, attackVolume);
    }
}
