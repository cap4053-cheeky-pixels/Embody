using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    //the target position of this enemy
    protected Transform target;
    public delegate void Died(GameObject who);
    public event Died deathEvent;


    private void Start()
    {
        MaxHealth = 1;
        Health = MaxHealth;
        Strength = 1;
        Speed = 3;

        //locate Player, let this be the target
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health != 0)
            Move();
    }

    public override void Move()
    {
        //face the player
        transform.LookAt(target);
        //move towards the player
        transform.position += transform.forward * Speed * Time.deltaTime;
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
        
        if (Health == 0 || Health + amount <= 0)
        {

            Health = 0;
           
            /* tag this enemy as dead, make it sleep for one frame (that is stop any forces applying on it indefinitely since it wont be awakened), and then
            disable the sphere collider by setting trigger to true. This has the added benefit of causing Player collision trigger routines to start.
            */
            gameObject.tag = "DeadDude";
            gameObject.GetComponent<Rigidbody>().Sleep();
            gameObject.GetComponent<Collider>().isTrigger = true;

            //the below line outlines the gameObject, to be used possibly when enemy is eligible for Possession.
            //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Outline", 1);

            deathEvent?.Invoke(gameObject);
            
        }
        else
            Health += amount;
    }
}
