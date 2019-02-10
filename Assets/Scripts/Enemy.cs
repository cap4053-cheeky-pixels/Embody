using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    //the target position of this enemy
    protected Transform target;

    public delegate void Died(GameObject who);
    public event Died deathEvent;

    public GameObject weapon;
    public float attemptFirerate = 0.5f;
    public int maxHealth;
    public int strength; // Not used
    public int speed;
    public float healthDropProbability;
    public GameObject HalfHeart;

    private bool showPossession = true;
    private float fireRateTimer = 0;
    private float possessTimer = 0;
    private IWeapon fireableWeapon;
    private bool movingEnabled;
    private CharacterController cc;

    private void Awake()
    {
        setWeapon(weapon);
        cc = GetComponent<CharacterController>();
    }
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
        fireRateTimer += Time.deltaTime;
        possessTimer += Time.deltaTime;

        if(Health != 0 && movingEnabled && target != null)
        {
            Move();
        }
        if(Health <= 0){
            
            possessTimer += Time.deltaTime;

            if (possessTimer > 1)
            {
                ToggleOutline();
                possessTimer = 0;
            }
            
        }
        if (fireRateTimer > attemptFirerate && movingEnabled && fireableWeapon != null)
        {
            fireableWeapon.Fire("Enemy-Fireball");
            fireRateTimer = 0;
        }
    }

    private void ToggleOutline()
    {
        if (showPossession)
        {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_Outline", .5f);
            showPossession = false;
        }
        else {
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_Outline", 0);
            showPossession = true;
        }
            
    }

    public void SetMovement(bool canMove)
    {
        movingEnabled = canMove;
    }

    public override void Move()
    {
        //face the player
        Vector3 tVector = target.position - transform.position;
        tVector.y = 0;
        transform.rotation = Quaternion.LookRotation(tVector);
        //move towards the player
        cc.SimpleMove(transform.forward * Speed);
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

            //the below line outlines the gameObject when enemy is eligible for Possession.
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetFloat("_Outline", .5f);

            deathEvent?.Invoke(gameObject);
            HealthDrop();
        }
    }

    public void setWeapon(GameObject weapon)
    {
        this.weapon = weapon;
        fireableWeapon = this.weapon.GetComponent<IWeapon>();
    }

}
