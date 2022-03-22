using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CogCollectible : MonoBehaviour
{
    public int cogValue;
    public float cogSpinSpeed;
    PauseComponent pC;

    //Reference to UI;
    

    // Start is called before the first frame update
    void Start()
    {
        pC = FindObjectOfType<PauseComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, 0f, Time.deltaTime * cogSpinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>()) 
        {
            //Increment UI Coin Value
            pC.incrementScore(cogValue);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
