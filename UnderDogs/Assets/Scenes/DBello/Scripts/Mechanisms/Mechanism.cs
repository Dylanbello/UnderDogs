using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    private Animator animator;
    //[Header("Write the name of the animation clip here")]
    
    //public string debugName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Animate(bool activate)
    {
        Debug.Log("animating" + activate);
        animator.SetBool("Activate", activate);
    }
}
