using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
/* APA7 Script
    THIS IS A APA7TH SCRIPT/CODE from https://uark.libguides.com/CSCE/CitingCode
Title: Ragdoll
Aurther: TheKiwiCoder
Date: <2020>
Availability: https://youtu.be/oLT4k-lrnwg
*/

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;

    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateRagdoll();
        }
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidBodies in rigidBodies)
        {
            rigidBodies.isKinematic = false;
        }
        animator.enabled = true;
    }

    public void ActivateRagdoll()
    {
        foreach(var rigidBodies in rigidBodies)
        {
            rigidBodies.isKinematic = false; 
        }
        animator.enabled = false;
    }
}