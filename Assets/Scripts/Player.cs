using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public delegate void HealthChanged();
    public static event HealthChanged healthChangedEvent;
    private bool collidingWithEnemy;

    private void Start()
    {
        MaxHealth = 6;
        Health = MaxHealth;
        Strength = 2;
        Speed = 20;
        collidingWithEnemy = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            collidingWithEnemy = true;
            StartCoroutine(ExchangeContactDamageWith(other));
        }
        else if(other.gameObject.tag == "Enemy-Fireball")
        {
            ChangeHealth(-1);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            collidingWithEnemy = false;
            StopCoroutine(ExchangeContactDamageWith(other));
        }
    }

    IEnumerator ExchangeContactDamageWith(Collider other)
    {
        while(collidingWithEnemy && other != null && this != null)
        {
            ChangeHealth(-1);
            other.gameObject.GetComponent<Enemy>().ChangeHealth(-1);
            yield return new WaitForSeconds(tickRate);
        }
    }
}
