using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMechanics : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject rocketSpawn;
    [SerializeField] private float rocketSpeed;
    void Update()
    {
        FollowMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FollowMouse()
    {
        Vector2 itemPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - itemPos;
        transform.right = direction;
    }

    void Shoot()
    {
        Vector2 itemPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - itemPos;

        GameObject newRocket = Instantiate(rocket, rocketSpawn.transform.position, transform.rotation);
        newRocket.GetComponent<Rigidbody2D>().velocity = direction.normalized * rocketSpeed;

        Destroy(newRocket, 3f);
    }
}
