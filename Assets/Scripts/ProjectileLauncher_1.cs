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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > interval)
        {
            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Z))
            {
                GameObject projectileInstance = Instantiate(projectile, transform.position + new Vector3(0, 2, 0), transform.rotation);

                Rigidbody rigidbody = projectileInstance.GetComponent<Rigidbody>();

                rigidbody.velocity = transform.forward * speed;

                Destroy(projectileInstance, projectileLifetime);

                timer = 0;
            }
        }
    }
}
