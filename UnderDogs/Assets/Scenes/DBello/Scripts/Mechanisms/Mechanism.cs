using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Animate(bool activate)
    {
        Debug.Log("animating" + activate);
        animator.SetBool("Activate", activate);

        //if (activate == true)
        //{
        //    audioSource.PlayDelayed(.2f);
        //}
       
    }
}
