using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
    public float heartSpinSpeed;
    //private AudioSource audioSource;
    //public HealthSystem playerHealth;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Rotate(Time.deltaTime * 0f, 0f, heartSpinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DogManager dog)) { dog.playerHealth.Heal(10);
            //audioSource.PlayDelayed(.2f);
            SoundManager.Play2DSound(SoundManager.Sound.HeartCollected);
            gameObject.SetActive(false);}

        /*if (other.GetComponent<CharacterController>())
        {
            //playerHealth.Heal(10);
            gameObject.SetActive(false);
            //playerHealth = other.GetComponent<HealthSystem>();
            //GameManager.Instance.AddToCollection(Value);
            
            

        }*/
    }
}
