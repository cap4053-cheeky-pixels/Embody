using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidantEnemy : Enemy
{

    public float speed = 5.0f;
    public bool spawned = false;

    // to be filled in through Inspector
    public Transform clone;

    void Start()
    {
        MaxHealth = 1;
        Health = MaxHealth;
        Strength = 1;
        Speed = 20;
        target = GameObject.FindGameObjectWithTag("Corner").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if (target.position.magnitude - transform.position.magnitude > .1f)
            transform.position += transform.forward * speed * Time.deltaTime;
        else
        {
            if (!spawned)
            {
                Instantiate(clone, GameObject.Find("FlySpawnPoint").transform.position, Quaternion.identity);
                Instantiate(clone, GameObject.Find("FlySpawnPoint2").transform.position, Quaternion.identity);
                spawned = true;
            }
        }
    }

}
