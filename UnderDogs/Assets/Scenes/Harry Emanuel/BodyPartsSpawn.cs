using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartsSpawn : MonoBehaviour
{
    Rigidbody rb;
    float dirX;
    float dirY;
    float dirZ;
   
    void Start()
    {
        dirX = Random.Range(-5, 5);
        dirY = Random.Range(-5, 8);
        dirZ = Random.Range(-20, 20);
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce (new Vector3(dirX, dirY,dirZ), ForceMode.Impulse);
    }

    private void Update(){
        DestroyObjectDelayed();
    }

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, 7);
    }
}
