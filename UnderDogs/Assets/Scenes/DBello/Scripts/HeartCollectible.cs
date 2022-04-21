using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    public float heartSpinSpeed;
    //public HealthSystem playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        transform.Rotate(Time.deltaTime * 0f, 0f, heartSpinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            //playerHealth.Heal(10);
            gameObject.SetActive(false);
            //playerHealth = other.GetComponent<HealthSystem>();
            //GameManager.Instance.AddToCollection(Value);
            
            

        }
    }
}
