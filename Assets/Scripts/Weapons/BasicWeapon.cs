using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour, IWeapon
{
    public GameObject projectile;
    public float speed = 15;
    public float interval = 1;
    public float projectileLifetime = 10;
    public float damage = 1;
    private float timer = 0;
    public float forwardOffset = 1.5f;
    public float upwardOffset = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    public void Fire(string tag)
    {
        if (timer > interval)
        {
            Debug.Log("Blap");
            Vector3 spawnPos =
                new Vector3(0, upwardOffset, 0) + transform.position + transform.forward * forwardOffset;
            GameObject projectileInstance =
                Instantiate(projectile, spawnPos, transform.rotation);

            Projectile pro = projectileInstance.GetComponent<Projectile>();
            pro.tag = tag;
            pro.velocity = transform.forward * speed;
            pro.damage = damage;

            Destroy(projectileInstance, projectileLifetime);

            timer = 0;
        }
    }
}
