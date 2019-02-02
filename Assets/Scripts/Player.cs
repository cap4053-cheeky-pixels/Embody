using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

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

    public override void ChangeHealth(int amount)
    {
        Health += amount;

        if (Health == 0)
        {
            // TODO give feedback to signal game over before deleting the player
            Destroy(gameObject);
        }
    }
}
