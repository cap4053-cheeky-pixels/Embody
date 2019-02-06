using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryLauncher : MonoBehaviour
{
    public float x_speed;
    public float y_speed;
    public float z_speed;

    public float projectileLifetime;

    public int interval;
    public GameObject projectile;

    private float timer;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > interval)
        {
            GameObject projectileInstance = Instantiate(projectile, transform.position + new Vector3(0,2,0), transform.rotation);

            Rigidbody rigidbody = projectileInstance.GetComponent<Rigidbody>();

            rigidbody.velocity = new Vector3(x_speed, y_speed, z_speed);

            Destroy(projectileInstance, projectileLifetime);

            timer = 0;
        }
    }
}
