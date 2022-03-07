using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
//This script tells the canvus to position anything in it towards the front of the camera as it will always follow it.
{
   public Transform cam1;
   //public Transform cam2;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam1.forward);
        //transform.LookAt(transform.position + cam2.forward);
    }
}
