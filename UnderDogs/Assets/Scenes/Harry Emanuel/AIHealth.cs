using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("AI/AIHealth")]
public class AIHealth : MonoBehaviour
{
    public int maxHeatlh = 100;
    public HealthSystem healthSystem;
    
    public GameObject head, body,L_Track,R_Track,L_Wheels,R_Wheels,Heart; //Prefabs that will Instantiate from enemy
    [SerializeField] GameObject explosionParticle;

    [Space(10)]
    [SerializeField] List<Slider> healthSliders;

    public float damageVolume;
    public float enemyDieVolume;

    void Awake() { SoundManager.Initialize();}

    void Start()
    {
        healthSystem = new HealthSystem(maxHeatlh);
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        healthSliders[0].maxValue = maxHeatlh;
        healthSliders[1].maxValue = maxHeatlh;

        
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) // This method is called when the AI's health changes via damage, healing, etc.
    {
        healthSliders[0].value = healthSystem.GetHealth();
        healthSliders[1].value = healthSystem.GetHealth();
        SoundManager.Play3DSound(SoundManager.Sound.EnemyTakeDamage, transform.position, damageVolume);

    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e) // This method is called when the AI dies.
    {
        partSpawns();
        SoundManager.Play3DSound(SoundManager.Sound.EnemyDie, transform.position, enemyDieVolume);
        Instantiate(Heart,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void partSpawns(){//Body parts that spawn with their attached script to fly off
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Instantiate(head, transform.position, Quaternion.identity);
        Instantiate(body, transform.position, Quaternion.identity);
        Instantiate(L_Track, transform.position, Quaternion.identity);
        Instantiate(R_Track, transform.position, Quaternion.identity);
        Instantiate(L_Wheels, transform.position, Quaternion.identity);
        Instantiate(R_Wheels, transform.position, Quaternion.identity);
    }
}
