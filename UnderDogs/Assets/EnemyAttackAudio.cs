using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAudio : MonoBehaviour
{
    public float attackVolume;
    public bool attack = false;
   

    public void OnEnable()
    {
        AttackSound();
    }


    public void AttackSound()
    {
        SoundManager.Play3DSound(SoundManager.Sound.EnemyAttack, transform.position, attackVolume);
    }
}
