using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public delegate void HealthChanged();
    public static event HealthChanged healthChangedEvent;

    private void Start()
    {
        MaxHealth = 6;
        Health = MaxHealth;
        Strength = 1;
        Speed = 20;
    }

    public override void Move()
    {
        // TODO code
    }

    public override void Attack()
    {
        // TODO code
    }

    public override void ChangeMaxHealth(int amount)
    {
        MaxHealth += amount;
        healthChangedEvent?.Invoke();
    }

    public override void ChangeHealth(int amount)
    {
        Health += amount;
        healthChangedEvent?.Invoke();

        if (Health == 0)
        {
            // TODO give feedback to signal game over before deleting the player
            Destroy(gameObject);
        }
    }

    // TODO remove once enemies and attacking have been implemented; in the meantime, this suffices for damage testing
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            ChangeHealth(-1);
        }
    }
}
