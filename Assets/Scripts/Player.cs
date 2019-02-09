using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int maxHealth;
    public int strength;
    public int speed;

    public delegate void HealthChanged();
    public event HealthChanged healthChangedEvent;
    private bool collidingWithEnemy;
    private ProjectileLauncher_1 shooter;

    private void Start()
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Strength = strength;
        Speed = speed;

        collidingWithEnemy = false;
        shooter = gameObject.GetComponent<ProjectileLauncher_1>();
    }

    private void Update()
    {
        Vector3 direction = transform.forward;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.forward = new Vector3(-1, 0, 0);
            shooter.Shoot();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.forward = new Vector3(1, 0, 0);
            shooter.Shoot();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.forward = new Vector3(0, 0, 1);
            shooter.Shoot();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.forward = new Vector3(0, 0, -1);
            shooter.Shoot();
        }
    }

    public override void Move()
    {
    }

    public override void Attack()
    {
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
            // TODO DONT DESTROY THE PLAYER! THIS THROWS EXCEPTIONS
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
        else if(other.gameObject.tag == "Half-Heart")
        {
            if(Health != MaxHealth)
            {
                ChangeHealth(1);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "DeadDude")
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
            if (other.gameObject.tag != "Spawner")
            {
                other.gameObject.GetComponent<Enemy>().ChangeHealth(-1);

                /* While colliding with an enemy, it may be that it has died and so is no longer an enemy. As a result, we must explicitly call
                OnTriggerExit
                */
                if (other.gameObject.tag == "DeadDude")
                    OnTriggerExit(other);
            }
            else
            {
                other.gameObject.GetComponent<AvoidantEnemy>().ChangeHealth(-1);

                /* While colliding with an enemy, it may be that it has died and so is no longer an enemy. As a result, we must explicitly call
                OnTriggerExit
                */
                if (other.gameObject.tag == "DeadDude")
                    OnTriggerExit(other);
            }
            yield return new WaitForSeconds(tickRate);
        }
    }
}
