using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;

    int healthMax;
    int health;

    public bool IsDead { get { return health <= 0; } }
    public float GetHealthPercent { get { return (float)health / healthMax; } }

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth() { return health; }
    public int GetMaxHealth() { return healthMax; }

    public void SetHealthAmount(int health)
    {
        this.health = health;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int amount)
    {
        health += amount;

        if (health >= healthMax) health = healthMax;

        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Die()
    {
        if (OnDead != null) OnDead(this, EventArgs.Empty);
    }
}
