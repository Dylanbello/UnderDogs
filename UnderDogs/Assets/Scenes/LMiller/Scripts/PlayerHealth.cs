using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    HealthSystem health;
    
    // Start is called before the first frame update
    void Start()
    {
        health = new HealthSystem(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            health.Damage(10);
            Debug.Log(health.GetHealthPercent);
        }
    }


}
