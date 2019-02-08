using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour, IWeapon
{
    public GameObject projectile;
    public float speed;
    public float interval;
    public float projectileLifetime;
    public float damage;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Fire()
    {
        if (timer > interval)
        {
            Debug.Log("Blap");
            Vector3 spawnPos =
                new Vector3(0, 1.5f, 0) + transform.position + transform.forward * 1;
            GameObject projectileInstance =
                Instantiate(projectile, spawnPos, transform.rotation);

            Projectile pro = projectileInstance.GetComponent<Projectile>();
            pro.velocity = transform.forward * speed;
            pro.damage = damage;

            Destroy(projectileInstance, projectileLifetime);

            timer = 0;
        }
    }
}
