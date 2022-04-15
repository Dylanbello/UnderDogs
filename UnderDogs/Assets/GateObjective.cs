using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateObjective : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Enemy1 == null) && (Enemy2 == null))
        {
            animator.SetBool("Activate", true);
        }
    }
}
