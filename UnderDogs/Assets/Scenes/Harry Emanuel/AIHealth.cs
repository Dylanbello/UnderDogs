using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHealth : MonoBehaviour
{
    public int maxHeatlh = 100;
    public HealthSystem healthSystem;
    ParticleSystem particleSystem;
    [Space(10)]
    public GameObject head, body,L_Track,R_Track,L_Wheels,R_Wheels;

    [Space(10)]
    [SerializeField] List<Slider> healthSliders;

    void Start()
    {
        healthSystem = new HealthSystem(maxHeatlh);
        particleSystem = GetComponent<ParticleSystem>();

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        healthSliders[0].maxValue = maxHeatlh;
        healthSliders[1].maxValue = maxHeatlh;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)    // This method is called when the AI's health changes via damage, healing, etc.
    {
        //Play injured particles.
        healthSliders[0].value = healthSystem.GetHealth();
        healthSliders[1].value = healthSystem.GetHealth();
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)     // This method is called when the AI dies.
    {
        partSpawns();
        Destroy(gameObject);
    }

    void partSpawns(){
        Instantiate(head, transform.position, Quaternion.identity);
        Instantiate(body, transform.position, Quaternion.identity);
        Instantiate(L_Track, transform.position, Quaternion.identity);
        Instantiate(R_Track, transform.position, Quaternion.identity);
        Instantiate(L_Wheels, transform.position, Quaternion.identity);
        Instantiate(R_Wheels, transform.position, Quaternion.identity);
    }
}
