using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidantEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform target;
    public float speed = 5.0f;
    public bool spawned = false;

    void Start()
    {
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
                Instantiate(GameObject.Find("Enemy"), GameObject.Find("FlySpawnPoint").transform.position, Quaternion.identity);
                Instantiate(GameObject.Find("Enemy"), GameObject.Find("FlySpawnPoint2").transform.position, Quaternion.identity);
                spawned = true;
            }
        }
    }

}
