using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogAttack : MonoBehaviour
{
    public float power = 1;
    public int attackDamage = 20;
    public float radius = 1;
    public float delay = 0;
    private float countdown;

    public AIHealth aIHealth;

    void Start()
    {
        countdown = delay;
    }

    void Update()
    {
        countdown -= Time.deltaTime; 
    }

    void Explode ()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null){
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);
                if (rb.gameObject.GetComponent<AIHealth>()){
                    rb.gameObject.GetComponent<AIHealth>().healthSystem.Damage(attackDamage);
                }
            }
        }
    }
    public void Attack(InputAction.CallbackContext context) { Explode(); }
}