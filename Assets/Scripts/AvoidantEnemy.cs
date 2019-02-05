using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidantEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform target;
    public float speed = 5.0f;
    public bool spawned = false;
    private GameObject enemy, FlySpawnPoint, FlySpawnPoint2;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Corner").transform;
        enemy = GameObject.Find("Enemy");
        FlySpawnPoint = GameObject.Find("FlySpawnPoint");
        FlySpawnPoint2 = GameObject.Find("FlySpawnPoint2");
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
                Instantiate(enemy, FlySpawnPoint.transform.position, Quaternion.identity);
                Instantiate(enemy, FlySpawnPoint2.transform.position, Quaternion.identity);
                spawned = true;
            }
        }
    }

}
