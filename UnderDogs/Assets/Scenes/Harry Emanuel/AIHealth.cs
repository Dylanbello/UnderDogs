using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    THIS IS A APA7TH SCRIPT/CODE from https://uark.libguides.com/CSCE/CitingCode
Title: AI Health
Aurther: Brackeys
Date: <2020>
Availability https://youtu.be/ieyHlYp5SLQ
*/
public class AIHealth : MonoBehaviour
{
    public Slider slider;
	public Gradient gradient;
	public Image fill;

    public int maxHeatlh = 100;
    public HealthSystem healthSystem;

    [Space(10), Tooltip("This is a list for the sliders seen by both player 1 and player 2")]
    public List<Slider> healthSliders;

    void Start()
    {
        healthSystem = new HealthSystem(maxHeatlh);

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        for (int i = 0; i < healthSliders.Count; i++) { healthSliders[i].maxValue = maxHeatlh; }

        Debug.Log(healthSystem.GetHealth());
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)    // This method is called when the AI's health changes via damage, healing, etc.
    {
        Debug.Log(healthSystem.GetHealth());
        for (int i = 0; i < healthSliders.Count; i++) { healthSliders[i].value = healthSystem.GetHealth(); }    // Sets healthbar value from health system.
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)     // This method is called when the AI dies.
    {
        //Wait for Delay Destory Object 
        //Play Particles
        //Play animation
        Destroy(gameObject);
    }

    public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth(int health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
