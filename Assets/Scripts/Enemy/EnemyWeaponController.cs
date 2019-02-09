using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject weapon;
    public float attemptFirerate = 0.5f;
    private IWeapon fireableWeapon;
    private float timer = 0;

    void setWeapon(GameObject weapon)
    {
        this.weapon = weapon;
        fireableWeapon = this.weapon.GetComponent<IWeapon>();
    }

    void Awake()
    {
        setWeapon(weapon);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fireableWeapon == null) return;

        timer += Time.deltaTime;
        if (timer > attemptFirerate)
        {
            fireableWeapon.Fire("Enemy-Fireball");
            timer = 0;
        }
    }
}
