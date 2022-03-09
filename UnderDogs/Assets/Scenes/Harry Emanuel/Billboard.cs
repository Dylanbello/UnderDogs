using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    THIS IS A APA7TH SCRIPT/CODE from https://uark.libguides.com/CSCE/CitingCode
Title: AI Health
Aurther: Brackeys
Date: <2020>
Availability https://youtu.be/ieyHlYp5SLQ
*/
public class Billboard : MonoBehaviour
{
   public Transform cam1;
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam1.forward);
    }
}
