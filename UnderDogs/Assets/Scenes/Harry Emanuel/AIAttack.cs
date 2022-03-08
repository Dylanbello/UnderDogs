using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{  
    [SerializeField] private float attackDamage = 50f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("character1")){
            if(attackSpeed <= canAttack){
                other.gameObject.GetComponent<DogsHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }else{
                canAttack += Time.deltaTime;
            }
        }
    }
}
