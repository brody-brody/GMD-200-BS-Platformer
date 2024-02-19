using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRocketSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    private bool shot = false;
    private bool started = false;
    private float rocketSpeed = 15f;

    void Update()
    {
        if (!shot)
        {
            StartCoroutine(RandomShot());
        }
    }

    IEnumerator RandomShot()
    {
        shot = true;
        // wait a few seconds for the items to drop into the scene
        if (!started)
        {
            yield return new WaitForSeconds(3f);
            started = true;
        }

        Vector2 direction = -transform.up;

        // choose a random position above the title off screen, spawn a rocket, and send it downward
        transform.position = new Vector2(Random.Range(-7.5f, 7.5f), transform.position.y);
        GameObject newRocket = Instantiate(rocket, transform.position, Quaternion.Euler(0, 0, -90f));
        newRocket.GetComponent<Rigidbody2D>().velocity = direction * rocketSpeed;

        yield return new WaitForSeconds(1.5f);

        shot = false;
    }
}
