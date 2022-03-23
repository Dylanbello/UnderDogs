using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElevatorControl : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "character1" && other.gameObject.tag == "character2") 
        {
            animator.SetBool("Ascending", true);
        }
    }


    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //}
}
