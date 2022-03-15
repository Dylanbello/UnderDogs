using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CogCollectible : MonoBehaviour
{
    public int cogValue;
    public float cogSpinSpeed;

    //Reference to UI;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, 0f, Time.deltaTime * cogSpinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInput>()) 
        {
            //Increment UI Coin Value
            Destroy(this.gameObject);
        }
    }
}
