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
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach (Collider2D other in inExplosionRadius)
        {
            if (!(other.CompareTag("Projectile")) && !(other.CompareTag("PushBlock")))
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 distance = other.transform.position - transform.position;
                    if (distance.magnitude > 0)
                    {
                        float explosionForce = ExplosionForceMulti / distance.magnitude;

                        if (SceneManager.GetActiveScene().name == "Title Menu")
                        {
                            Debug.Log("title");
                            rb.AddForce(distance.normalized * explosionForce * 2);
                        }
                        else
                            rb.AddForce(distance.normalized * explosionForce);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

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
