using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ailocomotion : MonoBehaviour
{
    public float maxTime, maxDistance = 1.0f;
    float timer = 0.0f;
    

    NavMeshAgent agent;

    public Transform playerTransform;
    Animator animator;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer-= Time.deltaTime;
        if(timer < 0.0f)
        {
            float sgdistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if(sgdistance > maxDistance*maxDistance)
            {
                //agent.destination = playerTransform.position;
            }
            timer = maxTime;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
