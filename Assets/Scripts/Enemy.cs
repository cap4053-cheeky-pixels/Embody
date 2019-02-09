﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //the target position of this enemy
    protected Transform target;
    private bool movingEnabled;

    public delegate void Died(GameObject who);
    public event Died deathEvent;

    public int maxHealth;
    public int strength;
    public int speed;
    public float healthDropProbability;
    public GameObject HalfHeart;


    private void Start()
    {
        MaxHealth = this.maxHealth;
        Health = MaxHealth;
        Strength = this.strength;
        Speed = this.speed;

        //locate Player, let this be the target
        target = GameObject.FindGameObjectWithTag("Player").transform;
        movingEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health != 0 && movingEnabled && target != null)
        {
            Move();
        }
    }

    public void SetMovement(bool canMove)
    {
        movingEnabled = canMove;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player-Fireball")
        {
            if (Health != 0)
            {
                ChangeHealth(-1);
                Destroy(other.gameObject);
            }
        }
    }

    void HealthDrop()
    {
        // calculate whether or not to drop
        var chance = Random.Range(0f, 1f);
        if(chance <= healthDropProbability)
        {
            var randX = Random.Range(-600, 600);
            var randZ = Random.Range(-600, 600);
            var heart = Instantiate(HalfHeart, transform.position + new Vector3(0, 2, 0), HalfHeart.transform.rotation);

            var heartRb = heart.GetComponent<Rigidbody>();

            heartRb.AddForce(new Vector3(randX, 800, randZ));
        }
    }

    public override void ChangeMaxHealth(int amount)
    {
        MaxHealth += amount;
    }

    public override void ChangeHealth(int amount)
    {
        Health += amount;

        if (Health <= 0)
        {
            /* tag this enemy as dead, make it sleep for one frame (that is stop any forces applying on it indefinitely since it wont be awakened), and then
            disable the sphere collider by setting trigger to true. This has the added benefit of causing Player collision trigger routines to start.
            */
            gameObject.tag = "DeadDude";
            gameObject.GetComponent<Collider>().isTrigger = true;

            //the below line outlines the gameObject, to be used possibly when enemy is eligible for Possession.
            //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Outline", 1);

            deathEvent?.Invoke(gameObject);
            HealthDrop();
        }
    }
}
