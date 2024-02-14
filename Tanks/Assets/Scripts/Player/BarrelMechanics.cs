using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMechanics : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject rocketSpawn;
    [SerializeField] private float rocketSpeed;
    [SerializeField] private float shootDelay = 0.5f;
    private bool delay = false;

    void Update()
    {
        FollowMouse();

        if (Input.GetMouseButtonDown(0) && !delay)
        {
            Shoot();
            StartCoroutine(ShootDelay());
        }
    }
    
    void FollowMouse()
    {
        if (!GameManager.paused)
        {
            Vector2 itemPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - itemPos;
            transform.right = direction;
        }
    }

    void Shoot()
    {
        if (!GameManager.paused)
        {
            Vector2 itemPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - itemPos;

            GameObject newRocket = Instantiate(rocket, rocketSpawn.transform.position, transform.rotation);
            newRocket.GetComponent<Rigidbody2D>().velocity = direction.normalized * rocketSpeed;

            Destroy(newRocket, 3f);
        }
    }

    IEnumerator ShootDelay()
    {
        delay = true;
        yield return new WaitForSeconds(shootDelay);
        delay = false;
    }
}
