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

        // if you left click and are not on delay
        if (Input.GetMouseButtonDown(0) && !delay)
        {
            Shoot();
            StartCoroutine(ShootDelay());
        }
    }
    
    void FollowMouse()
    {
        // as long as you are not paused
        if (!GameManager.paused)
        {
            // the mousoe position minus the player position will give a directional vector in which the tank barrel will point
            // in the direction of the mouse
            Vector2 itemPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - itemPos;
            transform.right = direction;
        }
    }

    void Shoot()
    {
        // as long as you arent paused
        if (!GameManager.paused)
        {
            // obtaining direction vector, exact same as FollowMouse()
            Vector2 itemPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - itemPos;

            // instantiate a new rocket with velocity
            // direction is normalized to keep velocity consistent no matter the original magnitude of the vector
            GameObject newRocket = Instantiate(rocket, rocketSpawn.transform.position, transform.rotation);
            newRocket.GetComponent<Rigidbody2D>().velocity = direction.normalized * rocketSpeed;

            Destroy(newRocket, 3f);
        }
    }

    // simple delay
    IEnumerator ShootDelay()
    {
        delay = true;
        yield return new WaitForSeconds(shootDelay);
        delay = false;
    }
}
