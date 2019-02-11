using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher_1 : MonoBehaviour
{
    public float speed;
    public GameObject projectile;
    public float interval;
    public float projectileLifetime;

    private float timer;

    public void Shoot()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            GameObject projectileInstance = Instantiate(projectile, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);

            Rigidbody rigidbody = projectileInstance.GetComponent<Rigidbody>();

            rigidbody.velocity = transform.forward * speed;

            Destroy(projectileInstance, projectileLifetime);

            timer = 0;
        }
    }
}
