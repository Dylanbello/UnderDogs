using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;

    int healthMax;
    int health;
    public int Health 
    {
        get { return health; }
        set 
        {
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            else if (health >= healthMax) { health = healthMax; }
            else { health = value; } 
        }
    }
    public bool IsDead { get { return Health <= 0; } }
    public float GetHealthPercent { get { return (float)Health / healthMax; } }

    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        Health = healthMax;
    }

    public void SetHealthAmount(int health)
    {
        this.Health = health;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Die()
    {
        if (OnDead != null) OnDead(this, EventArgs.Empty);
    }
}
