using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDies : MonoBehaviour
{
    public GameObject head, body,L_Track,R_Track,L_Wheels,R_Wheels;
    [SerializeField]private AudioSource lego;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            partSpawns();
        }
    }

    void partSpawns()
    {
        Instantiate(head, transform.position, Quaternion.identity);
        Instantiate(body, transform.position, Quaternion.identity);
        Instantiate(L_Track, transform.position, Quaternion.identity);
        Instantiate(R_Track, transform.position, Quaternion.identity);
        Instantiate(L_Wheels, transform.position, Quaternion.identity);
        Instantiate(R_Wheels, transform.position, Quaternion.identity);
        lego.Play();
    }
}