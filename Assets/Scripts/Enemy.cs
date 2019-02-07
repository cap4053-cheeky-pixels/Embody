using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //the target position of this enemy
    protected Transform target;

    private void Start()
    {
        MaxHealth = 5;
        Health = MaxHealth;
        Strength = 1;
        Speed = 3;

        //locate Player, let this be the target
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //face the player
        transform.LookAt(target);
        //movet towards the player
        transform.position += transform.forward * Speed * Time.deltaTime;
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
            // TODO make eligible for possession instead of destroying

            //the below line outlines the gameObject, to be used possibly when enemy has died.
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Outline", 1);
        }
    }
}
