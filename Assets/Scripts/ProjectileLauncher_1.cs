using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher_1 : MonoBehaviour
{
    public float speed;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectileInstance = Instantiate(projectile, transform.position, transform.rotation);
            GameObject player = GameObject.Find("Player");
            ////var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.x, player.transform.position.z));
            //var worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //worldMousePosition.z = 0;
            
            Rigidbody rigidbody = projectileInstance.GetComponent<Rigidbody>();
            ////rigidbody.velocity = worldMousePosition.normalized * speed;
            ///
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            projectileInstance.transform.position = player.transform.position + new Vector3(5, 5, 5);
            rigidbody.velocity = transform.TransformDirection(new Vector3(-90f, angle -90f, 0f)) * speed;
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
