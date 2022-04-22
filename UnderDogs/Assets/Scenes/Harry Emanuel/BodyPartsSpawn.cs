using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AI/BodyPartsSpawn")]
public class BodyPartsSpawn : MonoBehaviour
{
    Rigidbody rb;
    float dirX;
    float dirY;
    float dirZ;
   
    void Start()
    {
        dirX = Random.Range(-1,1);
        dirY = Random.Range(-1,1);
        dirZ = Random.Range(-7,7);
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce (new Vector3(dirX, dirY,dirZ), ForceMode.Impulse);
    }

    private void Update() { DestroyObjectDelayed(); } //call specific function in update

    void DestroyObjectDelayed() { Destroy(gameObject, 10); } //Game objects destory after 10 seconds
}
