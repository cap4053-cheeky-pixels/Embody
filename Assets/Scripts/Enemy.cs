using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private void Start()
    {
        MaxHealth = 8;
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
    }

    public override void ChangeHealth(int amount)
    {
        Health += amount;

        if (Health == 0)
        {
            // TODO make eligible for possession instead of destroying
            Destroy(gameObject);
        }
    }
}
