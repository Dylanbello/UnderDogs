using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5f;

    public void Move()
    {
        this.transform.position += this.transform.forward * Time.deltaTime * this.moveSpeed;
        Debug.Log("I Moved!");
    }

    public void MovedAgain()
    {
        this.transform.position -= this.transform.forward * Time.deltaTime * this.moveSpeed;
        Debug.Log("I Moved Again!");
    }

}
