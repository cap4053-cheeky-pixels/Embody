using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity;
    public float damage = 1;

    void OnTriggerEnter(Collider c)
    {
        // Remove  projectile only if it hits a wall
        if (c.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
