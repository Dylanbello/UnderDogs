using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DogAttack : MonoBehaviour
{
    public float power;
    public int grenadeDamage;
    public float radius;
    public float delay = 3f;
    private float countdown;
    bool hasExploded = false;


    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)//TODO: if the dog has attacked to the explode
        {
            Explode();
            hasExploded = true;
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
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);

                /*if (rb.gameObject.GetComponent<AIHealth>())
                {
                    rb.gameObject.GetComponent<AIHealth>().TakeDamage(grenadeDamage);//TODO change Takadamage feild
                }
                */
            }
        }
    }
}