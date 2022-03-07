using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DogAttack : MonoBehaviour
{
    public float power;
    public int grenadeDamage;
    public float radius;
    public float delay = 0f;
    private float countdown;
    //bool hasExploded = false;

    public AIHealth aIHealth;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X))//TODO: if the dog has attacked to the explode
        {
            Explode();
            //hasExploded = true;
            Debug.Log("Dog Attacked");
        }
    }

    void Explode ()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Debug.Log("Enemy Hit");
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);

                if (rb.gameObject.GetComponent<AIHealth>())
                {
                    rb.gameObject.GetComponent<AIHealth>().TakeDamage(1);
                    Debug.Log("Enemy Attacked");
                
                }
                
            }
        }
    }
}