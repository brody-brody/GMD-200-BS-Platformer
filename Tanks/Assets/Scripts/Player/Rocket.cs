using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Explosions moving other objects in scene adapted from
// https://www.youtube.com/watch?v=mbX4FbDhx30 (60 Seconds Tutorial | Explosions (Physics Based) | Unity 2D)

public class Rocket : MonoBehaviour
{
    [SerializeField] private float ExplosionForceMulti = 5;
    [SerializeField] private float ExplosionRadius = 5;
    [SerializeField] private GameObject explodeParticle;
    Collider2D[] inExplosionRadius = null;

    // rocket explodes on a collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.CompareTag("Player")))
        {
            StartCoroutine(RunExplosion());
            Instantiate(explodeParticle, transform.position, Quaternion.identity);
        }
    }

    void Explode()
    {
        // get all objects with colliders within the circle drawn
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        // for each collider that was grabbed by the overlap
        foreach (Collider2D other in inExplosionRadius)
        {
            // if the object is not another rocket or a push block
            if (!(other.CompareTag("Projectile")) && !(other.CompareTag("PushBlock")))
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // get direction from the point of impact to the collider
                    Vector2 distance = other.transform.position - transform.position;
                    if (distance.magnitude > 0)
                    {
                        // assign explosion force based on distance from the initial impact
                        float explosionForce = ExplosionForceMulti / distance.magnitude;

                        // add more force if you're on the title screen because it looks fun, otherwise dont
                        if (SceneManager.GetActiveScene().name == "Title Menu")
                        {
                            rb.AddForce(distance.normalized * explosionForce * 2);
                        }
                        else
                            rb.AddForce(distance.normalized * explosionForce);
                    }
                }
            }
        }
    }

    // editor drawing
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

    // run explosion will remove the rocket after half a second, but keep it there for calculations until the half a second is up
    IEnumerator RunExplosion()
    {
        Color transparent = new Color(0f, 0f, 0f, 0f);
        Explode();
        gameObject.GetComponent<SpriteRenderer>().color = transparent;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Destroy(gameObject, 0.5f);

        yield return null;
    }
}
