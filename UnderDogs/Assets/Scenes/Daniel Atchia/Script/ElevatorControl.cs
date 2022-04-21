using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElevatorControl : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float time;
    public float duration;
    public bool isMoving = false;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(isMoving == true)
        {
            float time = 0;
            while (time < duration)
            {
                float t = time / duration;
                //t = t * t * (3f - 2f * t);
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                time += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "character1" && other.gameObject.tag == "character2") 
        {
            isMoving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "character1" && other.gameObject.tag == "character2")
        {
            isMoving = false;
        }
    }


    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{

    //}
}
