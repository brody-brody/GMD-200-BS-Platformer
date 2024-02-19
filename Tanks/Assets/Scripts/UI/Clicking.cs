using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clicking : MonoBehaviour
{
    public GameObject playButton;
    private SpriteRenderer playSpr;
    public Sprite clickPlay;
    public bool mouseClick;
    public static bool started = false;

    void Start()
    {
        playSpr = playButton.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // make the small gameobject follow the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ClickDelay());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Play"))
        {
            // if you click while you are over the play button, you will enter the scene and play the game
            if (mouseClick)
            {
                playSpr.sprite = clickPlay;
                playButton.transform.position += new Vector3(0, -0.35f, 0);
                started = true;
                SceneManager.LoadScene("Level1");
            }
        }
    }

    // prevent click spamming as to not mess up the button
    IEnumerator ClickDelay()
    {
        mouseClick = true;
        yield return new WaitForSeconds(0.1f);
        mouseClick = false;
    }
}
