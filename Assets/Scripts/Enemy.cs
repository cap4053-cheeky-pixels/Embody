using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public delegate void Died(GameObject who);
    public event Died deathEvent;

    private void Start()
    {
        MaxHealth = 1;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player-Fireball")
        {
            ChangeHealth(-1);
            Destroy(other.gameObject);
        }
    }

    public override void ChangeMaxHealth(int amount)
    {
        MaxHealth += amount;
    }

    public override void ChangeHealth(int amount)
    {
        Health += amount;

        if (Health == 0)
        {
            deathEvent?.Invoke(gameObject);
            // TODO make eligible for possession instead of destroying
            Destroy(gameObject);
        }
    }
}
