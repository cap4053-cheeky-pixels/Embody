using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadWeapon : MonoBehaviour, IWeapon
{
    public GameObject projectile;
    public float speed = 15;
    public float interval = 1.5f;
    public float projectileLifetime = 10;
    public float damage = 1;
    public float forwardOffset = 1.5f;
    public float upwardOffset = 1.5f;
    private float timer = 0;
    private float angularOffset = 90;
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
            // Shoot 4 balls at "angularOffset" degrees displacement from center
            Vector3 forward = transform.forward.normalized;
            Vector3 forwardLeftPos = Quaternion.Euler(0, -1 * angularOffset, 0) * forward;
            Vector3 forwardRightPos = Quaternion.Euler(0, angularOffset, 0) * forward;
            Vector3 backwardPos = Quaternion.Euler(0, angularOffset*2, 0) * forward;
            Vector3 offset = new Vector3(0, upwardOffset, 0);

            Vector3 spawnForwardPos = offset + transform.position + forward * forwardOffset;
            Vector3 spawnLeftPos = offset + transform.position + forwardLeftPos * forwardOffset;
            Vector3 spawnRightPos = offset + transform.position + forwardRightPos * forwardOffset;
            Vector3 spawnBackwardPos = offset + transform.position + backwardPos * forwardOffset;

            GameObject projectileInstance =
                Instantiate(projectile, spawnForwardPos, transform.rotation);
            projectileInstance.tag = tag;
            Projectile pro = projectileInstance.GetComponent<Projectile>();
            pro.velocity = forward * speed;
            pro.damage = damage;
            Destroy(projectileInstance, projectileLifetime);

            GameObject projectileLeftInstance =
                Instantiate(projectile, spawnLeftPos, transform.rotation);
            projectileLeftInstance.tag = tag;
            Projectile proLeft = projectileLeftInstance.GetComponent<Projectile>();
            proLeft.velocity = forwardLeftPos * speed;
            proLeft.damage = damage;
            Destroy(projectileLeftInstance, projectileLifetime);

            GameObject projectileRightInstance =
                Instantiate(projectile, spawnRightPos, transform.rotation);
            projectileRightInstance.tag = tag;
            Projectile proRight = projectileRightInstance.GetComponent<Projectile>();
            proRight.velocity = forwardRightPos * speed;
            proRight.damage = damage;
            Destroy(projectileRightInstance, projectileLifetime);

            GameObject projectileBackwardInstance =
                Instantiate(projectile, spawnBackwardPos, transform.rotation);
            projectileBackwardInstance.tag = tag;
            Projectile proBackward = projectileBackwardInstance.GetComponent<Projectile>();
            proBackward.velocity = backwardPos * speed;
            proBackward.damage = damage;
            Destroy(projectileBackwardInstance, projectileLifetime);

            timer = 0;
        }
    }
}
